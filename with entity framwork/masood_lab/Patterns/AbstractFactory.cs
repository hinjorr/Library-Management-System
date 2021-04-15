using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using masood_lab.Models;
namespace masood_lab.AbstractFactory
{
    
    //abstractfactory
    public interface IAbstractFactory
    {
        ITextBooks GetTextBooks();

        IRefrenceBooks GetRefrenceBooks();
    }

    //factory1
    class EngineeringFactory : IAbstractFactory
    {
        public ITextBooks GetTextBooks()
        {
            return new EngineeringTextBooks();
        }

        public IRefrenceBooks GetRefrenceBooks()
        {
            return new EngineeringREfrenceBooks();
        }
    }

    //factory2
    class CSFactory : IAbstractFactory
    {
        public ITextBooks GetTextBooks()
        {
            return new CSTextBooks();
        }

        public IRefrenceBooks GetRefrenceBooks()
        {
            return new CSRefrenceBooks();
        }
    }

    
    
    
    //productA
    public interface ITextBooks
    {
        void GetTextbook();
    }

    //prodcuctA1
    class EngineeringTextBooks : ITextBooks
    {
        public void GetTextbook()
        {
            
        }
    }

    //prodcuctA2
    class CSTextBooks : ITextBooks
    {
        public void GetTextbook()
        {
        }
    }


    //productB
    public interface IRefrenceBooks
    {
        void GetRefrenceBook();
        
       
    }

    //productB1
    class EngineeringREfrenceBooks : IRefrenceBooks
    {
        


        public void GetRefrenceBook()
        {
           
        }

      
        
    }


    //productB2
    class CSRefrenceBooks : IRefrenceBooks
    {
        public void GetRefrenceBook()
        {
            throw new NotImplementedException();
        }
    }


    


}