using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using GldaniParkService.DataAccess;
using GldaniParkService.Models;

namespace GldaniParkService.Services
{
    public class OperationService
    {
        public bool SaveCard(Card card)
        {
            try
            {
                DataAccess<Card> d = new DataAccess<Card>();
            //    d.Save("spSaveCard", card);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool SaveAttraction(Attraction attraction)
        {
            try
            {
                DataAccess<Attraction> d = new DataAccess<Attraction>();

                var param = new DynamicParameters();
                param.Add("@Name", attraction.Name);
                param.Add("@AttractionId", attraction.AttractionId);
                param.Add("@Price", attraction.Price);

                d.Save("spSaveAttraction", param);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public bool SaveTransaction(Transaction transaction)
        {
            try
            {
                DataAccess<Transaction> d = new DataAccess<Transaction>();
           //     d.Save("spSaveTransaction", transaction);
            }
            catch
            {
                return false;
            }
            return true;
        }


        public IEnumerable<Card> GetCards()
        {

            DataAccess<Card> d = new DataAccess<Card>();
            var cardList = d.Get("spGetCardList");
            return cardList;
        }


        public IEnumerable<Attraction> GetAttractions()
        {

            DataAccess<Attraction> d = new DataAccess<Attraction>();
            var attractions = d.Get("spGetAttractionList");
            return attractions;
        }


        public Attraction GetAttraction(int id)
        {
            DataAccess<Attraction> d = new DataAccess<Attraction>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@Id", id);
            var attractions = d.GetById("spGetAttraction", param);
            return attractions;
        }


        public Card GetCard(int id)
        {
            DataAccess<Card> d = new DataAccess<Card>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@Id", id);
            var card = d.GetById("spGetCard", param);
            return card;
        }

        public bool DeleteTransaction(int id)
        {
            try
            {
                DataAccess<Transaction> d = new DataAccess<Transaction>();
                DynamicParameters param = new DynamicParameters();
                param.Add("@Id", id);
                d.DeleteById("spDeleteAttraction", param);
            }
            catch
            {
                return false;
            }
            return true;
        }


        public bool UpdateAttraction(Attraction attraction)
        {
            try
            {
                DataAccess<Attraction> d = new DataAccess<Attraction>();

                var param = new DynamicParameters();
                param.Add("@Name", attraction.Name); 
                param.Add("@Price", attraction.Price);
                param.Add("@Id", attraction.Id);

                d.Save("spUpdateAttraction", param);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

    }
}
