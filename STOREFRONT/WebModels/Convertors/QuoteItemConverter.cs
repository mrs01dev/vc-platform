﻿using System.Collections.Generic;
using System.Linq;
using VirtoCommerce.Web.Models;
using DataContracts = VirtoCommerce.ApiClient.DataContracts;

namespace VirtoCommerce.Web.Convertors
{
    public static class QuoteItemConverter
    {
        public static QuoteItem ToQuoteItem(this Product productModel)
        {
            var quoteItemModel = new QuoteItem();

            var variantModel = productModel.Variants.First();

            decimal price = (decimal)variantModel.Price / 100;

            quoteItemModel.CatalogId = "fake";
            quoteItemModel.CategoryId = "fake";
            quoteItemModel.ImageUrl = variantModel.Image.Src;
            quoteItemModel.ListPrice = price;
            quoteItemModel.ProductId = productModel.Id;
            quoteItemModel.SalePrice = price;

            quoteItemModel.ProposalPrices.Add(new TierPrice { Quantity = 1, Price = price });

            quoteItemModel.Title = productModel.Title;

            return quoteItemModel;
        }

        public static QuoteItem ToViewModel(this DataContracts.Quotes.QuoteItem quoteItem)
        {
            var quoteItemModel = new QuoteItem();

            quoteItemModel.CatalogId = quoteItem.CatalogId;
            quoteItemModel.CategoryId = quoteItem.CategoryId;
            quoteItemModel.Comment = quoteItem.Comment;
            quoteItemModel.Id = quoteItem.Id;
            quoteItemModel.ImageUrl = quoteItem.ImageUrl;
            quoteItemModel.ListPrice = quoteItem.ListPrice;
            quoteItemModel.ProductId = quoteItem.ProductId;

            if (quoteItem.ProposalPrices != null)
            {
                foreach (var proposalPrice in quoteItem.ProposalPrices)
                {
                    quoteItemModel.ProposalPrices.Add(new TierPrice
                    {
                        Price = proposalPrice.Price,
                        Quantity = proposalPrice.Quantity
                    });
                }
            }

            quoteItemModel.SalePrice = quoteItem.SalePrice;

            if (quoteItem.SelectedTierPrice != null)
            {
                quoteItemModel.SelectedTierPrice = new TierPrice
                {
                    Quantity = quoteItem.SelectedTierPrice.Quantity,
                    Price = quoteItem.SelectedTierPrice.Price
                };
            }

            quoteItemModel.Title = quoteItem.Name;

            return quoteItemModel;
        }

        public static DataContracts.Quotes.QuoteItem ToServiceModel(this QuoteItem quoteItemModel)
        {
            var quoteItem = new DataContracts.Quotes.QuoteItem();

            quoteItem.CatalogId = quoteItemModel.CatalogId;
            quoteItem.CategoryId = quoteItemModel.CategoryId;
            quoteItem.Comment = quoteItemModel.Comment;
            quoteItem.Id = quoteItemModel.Id;
            quoteItem.ImageUrl = quoteItemModel.ImageUrl;
            quoteItem.ListPrice = quoteItemModel.ListPrice;
            quoteItem.Name = quoteItemModel.Title;
            quoteItem.ProductId = quoteItemModel.ProductId;

            quoteItem.ProposalPrices = new List<DataContracts.Quotes.TierPrice>();
            foreach (var proposalPriceModel in quoteItemModel.ProposalPrices)
            {
                quoteItem.ProposalPrices.Add(new DataContracts.Quotes.TierPrice
                {
                    Price = proposalPriceModel.Price,
                    Quantity = proposalPriceModel.Quantity
                });
            }

            if (quoteItemModel.SelectedTierPrice != null)
            {
                quoteItem.SelectedTierPrice = new DataContracts.Quotes.TierPrice
                {
                    Quantity = quoteItemModel.SelectedTierPrice.Quantity,
                    Price = quoteItemModel.SelectedTierPrice.Price
                };
            }

            quoteItem.SalePrice = quoteItemModel.SalePrice;
            quoteItem.TaxType = null; // TODO

            return quoteItem;
        }
    }
}