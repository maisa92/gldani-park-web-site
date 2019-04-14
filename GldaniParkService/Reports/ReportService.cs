using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GldaniParkService.DataAccess;
using GldaniParkService.Enums;
using GldaniParkService.Models;
using GldaniParkService.Models.HelperModels;
using GldaniParkService.Models.ReportModels;

namespace GldaniParkService.Reports
{
    public class ReportService
    {

        // "ხარჯების ანალიზი"
        public List<CostAnalysisResponse> CostAnalysis(DateTime startDate, DateTime endDate)
        {
            DataAccess<CostAnalysisResponse> d = new DataAccess<CostAnalysisResponse>();
            return d.Get("spCostAnalysis", startDate, endDate).ToList();
        }

        // შემოსული თანხების ანალიზი
        public List<AnalysisOfIncomingFundsResponse> AnalysisOfIncomingFunds(DateTime startDate, DateTime endDate)
        {
            DataAccess<AnalysisOfIncomingFundsResponse> d = new DataAccess<AnalysisOfIncomingFundsResponse>();
            return d.Get("spAnalysisOfIncomingFunds", startDate, endDate).ToList();
        }

        // "თანხების საათობრივი განაწილება"
        public List<HourlyDistributionOfFundsResponse> HourlyDistributionOfFunds(DateTime startDate, DateTime endDate)
        {
            DataAccess<HourlyDistributionOfFundsModel> d = new DataAccess<HourlyDistributionOfFundsModel>();
            List<HourlyDistributionOfFundsModel> list = d.Get("spHourlyDistributionOfFunds", startDate, endDate).ToList();

            var model = list.Select(x => new HourlyDistributionOfFundsResponse()
            {
                WeekDay = x.TransactionDate.DayOfWeek.ToString(),
                Hour = $"{x.TransactionDate.DayOfWeek}  {x.TransactionDate.Hour}:00 - {x.TransactionDate.AddHours(1).Hour}:00",
                Income = !x.IsOutCome ? x.Amount : 0,
                OutCome = x.IsOutCome ? x.Amount : 0,
                CardAmount = !x.IsOutCome ? 1 : 0,
                CardId = x.CardId
            });

            var returnList = model.GroupBy(x => x.Hour).Select(y => new HourlyDistributionOfFundsResponse()
            {

                Income = y.Sum(c => c.Income),
                OutCome = y.Sum(c => c.OutCome),
                CardAmount = y.Sum(c => c.CardAmount),
                ActingCardAmount = y.Select(c => c.CardId).Distinct().Count()

            });

            return returnList.ToList();
        }

        // შემოსული თანხების გადანაწილება თვეებზე
        public List<DistributionOfTheAmountOfMoneyReceivedInMonthsResponse> DistributionOfTheAmountOfMoneyReceivedInMonths(int year, int month)
        {
            DataAccess<DistributionOfTheAmountOfMoneyReceivedInMonthsAmount> d = new DataAccess<DistributionOfTheAmountOfMoneyReceivedInMonthsAmount>();
            List<DateTime> dateList = new List<DateTime>();
            DateTime startDate = new DateTime(year, month, 1);
            DateTime endDate = new DateTime(year, month, startDate.AddMonths(1).AddDays(-1).Day);

            foreach (DateTime day in EachDay(startDate, endDate))
            {
                dateList.Add(day);
            }
            List<DistributionOfTheAmountOfMoneyReceivedInMonthsResponse> returnModel = new List<DistributionOfTheAmountOfMoneyReceivedInMonthsResponse>();

            foreach (var firstDay in dateList)
            {
                DistributionOfTheAmountOfMoneyReceivedInMonthsAmount amount = d.Get("spDistributionOfTheAmountOfMoneyReceivedInMonths", firstDay, firstDay.AddDays(1)).FirstOrDefault();
                DistributionOfTheAmountOfMoneyReceivedInMonthsResponse model = new DistributionOfTheAmountOfMoneyReceivedInMonthsResponse();
                model.Date = firstDay;
                model.Income = amount?.Amount ?? 0;
                returnModel.Add(model);
            }
            return returnModel;
        }

        private IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }



        // ბარათის გაყიდვა 
        public List<CardsSellResponse> CardsSell(DateTime startDate, DateTime endDate)
        {
            DataAccess<CardsSellResponse> d = new DataAccess<CardsSellResponse>();
            return d.Get("spCardsSell", startDate, endDate).ToList();
        }

        // ფულის მოძრაობა ბარათზე
        public List<MoneyMovemenResponse> MoneyMovement(DateTime startDate, DateTime endDate)
        {
            DataAccess<MoneyMovemenResponse> d = new DataAccess<MoneyMovemenResponse>();
            return d.Get("spMoneyMovemen", startDate, endDate).ToList();
        }

        // ბარათის ისტორია
        public CardHistoryResponse CardHistory(string cardId)
        {
            CardHistoryResponse historyResponse = new CardHistoryResponse();

            DataAccess<Transaction> d = new DataAccess<Transaction>();
            var cardTransactionDetail = d.Get("spCardHistoryTransactions", cardId).ToList();

            DataAccess<CardHistoryHelperModel> db = new DataAccess<CardHistoryHelperModel>();
            var cardDetail = db.Get("spCardHistory", cardId).FirstOrDefault();


            DataAccess<Attraction> attractionAccess = new DataAccess<Attraction>();
            var attractionList = attractionAccess.Get("spGetAttractionList").ToList();

            if (cardDetail != null)
            {
                historyResponse.CardId = cardDetail.CardId;
                historyResponse.CardPrice = cardDetail.CardPrice;
                historyResponse.CreateDate = cardDetail.CreateDate;
                historyResponse.RestAmount = cardDetail.RestAmount;
                historyResponse.IsActive = cardDetail.IsActive;
            }
            foreach (var item in cardTransactionDetail)
            {
                if (item.Type)
                {
                    if (item.TransactionDate != null)
                    {
                        var firstOrDefault = attractionList.FirstOrDefault(x => x.AttractionId == item.AttractionId);
                        if (firstOrDefault != null)
                            if (item.AttractionId != null)
                                historyResponse.AttractionList.AddRange(new List<AttractionForCardHistoryModel>()
                                {
                                    new AttractionForCardHistoryModel()
                                    {
                                        Date = (DateTime) item.TransactionDate,
                                        Name = firstOrDefault.Name,
                                        Id =   item.AttractionId
                                    }
                                });
                    }
                }
            }

            historyResponse.TransactionList = cardTransactionDetail;

            return historyResponse;
        }

        // ბარათის ტრანზაქციები
        public List<CardTransactionsResponse> CardTransactions(DateTime startDate, DateTime endDate)
        {
            DataAccess<CardTransactionsResponse> d = new DataAccess<CardTransactionsResponse>();
            return d.Get("spCardTransactions", startDate, endDate).ToList();
        }


        // ანალიზი ატრაქციონებზე თამაშის
        public List<AttractionAnalysisResponse> AttractionAnalysis(DateTime startDate, DateTime endDate)
        {
            DataAccess<Transaction> d = new DataAccess<Transaction>();
            var attractionTransactionList = d.Get("spAttractionAnalysis", startDate, endDate).ToList();

            DataAccess<Attraction> attractionAccess = new DataAccess<Attraction>();
            var attractionList = attractionAccess.Get("spGetAttractionList").ToList();


            var returnResponse = new List<AttractionAnalysisResponse>();
            foreach (var itmeAttraction in attractionList)
            {
                returnResponse.Add(
                    new AttractionAnalysisResponse()
                    {
                        AnalysList = new List<AttractionAnalysis>()
                       {
                           new AttractionAnalysis()
                           {
                               Name = itmeAttraction.Name,
                               Amount = attractionTransactionList.Where(x => x.AttractionId == itmeAttraction.AttractionId).Sum(y => y.Amount)
                           }
                       }
                    });
            }

            foreach (var item in returnResponse)
            {
                item.ActiveAttractionCount = item.AnalysList.Count(x => x.Amount != 0);
            }

            return returnResponse;
        }

        // აპარატების რეიტინგი 

        public List<AttractionRankResponse> AttractionRank(DateTime startDate, DateTime endDate)
        {
            DataAccess<Attraction> attractionAccess = new DataAccess<Attraction>();
            var attractionList = attractionAccess.Get("spGetAttractionList").ToList();

            DataAccess<Transaction> d = new DataAccess<Transaction>();
            var attractionTransactionList = d.Get("spAttractionAnalysis", startDate, endDate).ToList();

            List<AttractionRankResponse> returnResponse = new List<AttractionRankResponse>();

            foreach (var item in attractionList)
            {

                returnResponse.Add(new AttractionRankResponse()
                {
                    Name = item.Name,
                    Amount = attractionTransactionList.Where(x => x.AttractionId == item.AttractionId).Sum(y => y.Amount)
                });
            }

            return returnResponse;
        }
    }
}



















