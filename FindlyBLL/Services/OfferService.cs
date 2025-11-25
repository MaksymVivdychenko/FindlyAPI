using System.Runtime.InteropServices.JavaScript;
using System.Xml;
using AutoMapper;
using FindlyBLL.DTOs;
using FindlyBLL.DTOs.OffersDto;
using FindlyBLL.Interfaces;
using FindlyDAL.DB;
using FindlyDAL.Entities;
using FindlyDAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FindlyBLL.Services;

public class OfferService : IOfferService
{
    private readonly IOfferRepository _offerRepo;
    private readonly ILikedOfferRepository _likedOfferRepo;
    private readonly IMapper _mapper;
    private readonly IBookRepository _bookRepo;
    private readonly IUserRepository _userRepo;

    public OfferService(IOfferRepository offerRepo,
        ILikedOfferRepository likedOfferRepo, IMapper mapper, IBookRepository bookRepo,
        IUserRepository userRepo)
    {
        _offerRepo = offerRepo;
        _likedOfferRepo = likedOfferRepo;
        _mapper = mapper;
        _bookRepo = bookRepo;
        _userRepo = userRepo;
    }

    public async Task<List<OfferDto>> GetOffersByBookId(Guid bookId, Guid? userId)
    {
        if (await _bookRepo.GetByIdAsync(bookId) == null)
        {
            throw new Exception("book doesn't exist");
        }
        var offers = await _offerRepo.GetOffersForBook(bookId);
        List<OfferDto> offersDto = _mapper.Map<List<OfferDto>>(offers);
        if (userId == null)
        {
            return offersDto;
        }
        var offerIds = offersDto.Select(o => o.Id).ToList();
        var likedOffers = await _likedOfferRepo.FindAsync(q => 
            q.UserId == userId && offerIds.Contains(q.OfferId));
        var likedOffersDict = likedOffers.ToDictionary(lo => lo.OfferId);
        foreach (var offerDto in offersDto)
        {
            if (likedOffersDict.TryGetValue(offerDto.Id, out var likedOffer))
            {
                offerDto.IsLiked = true;
                if (likedOffer.PriceToNotify != null)
                {
                    offerDto.IsPriceSet = true;
                }
            }
        }
        return offersDto;
    }

    public async Task AddOfferToFavorite(Guid userId, Guid offerId)
    {
        var offer = await _offerRepo.GetByIdAsync(offerId);
        if (offer == null)
        {
            throw new Exception("offer doesn't exist");
        }
        var user = await _userRepo.GetByIdAsync(userId);
        if (user == null)
        {
            throw new Exception("user doesn't exist");
        }
        UserLikedOffers likedOffer = new UserLikedOffers { OfferId = offerId, UserId = userId };
        await _likedOfferRepo.AddAsync(likedOffer);
    }

    public async Task RemoveOfferFromFavorite(Guid userId, Guid offerId)
    {
        var likedOffer = (await _likedOfferRepo.FindAsync(q =>  q.UserId == userId &&
                                                               q.OfferId == offerId)).FirstOrDefault() !;
        await _likedOfferRepo.DeleteAsync(likedOffer);
    }

    public async Task AddPriceToNotify(Guid userId, Guid offerId, decimal price)
    {
        var likedOffer = (await _likedOfferRepo.FindAsync(q =>  q.UserId == userId &&
                                                                q.OfferId == offerId)).FirstOrDefault() !;
        likedOffer!.PriceToNotify = price;
        await _likedOfferRepo.UpdateAsync(likedOffer);
    }

    public async Task RemoveNotify(Guid userId, Guid offerId)
    {
        var likedOffer = (await _likedOfferRepo.FindAsync(q =>  q.UserId == userId &&
                                                                q.OfferId == offerId)).FirstOrDefault() !;
        likedOffer!.PriceToNotify = null;
        await _likedOfferRepo.UpdateAsync(likedOffer);
    }

    public async Task<List<LikedOfferDto>> GetLikedOfferForUser(Guid userId)
    {
        if ((await _userRepo.GetByIdAsync(userId)) == null)
        {
            throw new Exception("User doesn't exist");
        }
        var likedOffers = (await _likedOfferRepo.GetLikedOffersByUserId(userId)).ToList();
        return _mapper.Map<List<LikedOfferDto>>(likedOffers);
    }
}