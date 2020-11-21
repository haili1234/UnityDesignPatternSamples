//-------------------------------------------------------------------------------------
//	ObserverExample1.cs
//-------------------------------------------------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//This real-world code demonstrates the Observer pattern in which registered investors are notified every time a stock changes value.
//该实际代码演示了观察者模式，在该模式中，每次股票变化时都会通知注册投资者
namespace ObserverExample1
{
    public class ObserverExample1 : MonoBehaviour
    {
        void Start()
        {
            // Create IBM stock and attach investors
            //创建IBM股票并吸引投资者
            IBM ibm = new IBM("IBM", 120.00);
            ibm.Attach(new Investor("Sorros"));
            ibm.Attach(new Investor("Berkshire"));

            // Fluctuating prices will notify investors
            ibm.Price = 120.10;
            ibm.Price = 121.00;
            ibm.Price = 120.50;
            ibm.Price = 120.75;
        }
    }

    /// <summary>
    /// The 'Subject' abstract class
    /// </summary>
    abstract class Stock
    {
        private string _symbol;
        private double _price;
        private List<IInvestor> _investors = new List<IInvestor>();

        // Constructor
        public Stock(string symbol, double price)
        {
            this._symbol = symbol;
            this._price = price;
        }

        /// <summary>
        /// 链接观察者
        /// </summary>
        /// <param name="investor"></param>
        public void Attach(IInvestor investor)
        {
            _investors.Add(investor);
        }

        /// <summary>
        /// 分离观察者
        /// </summary>
        /// <param name="investor"></param>
        public void Detach(IInvestor investor)
        {
            _investors.Remove(investor);
        }

        public void Notify()
        {
            foreach (IInvestor investor in _investors)
            {
                investor.Update(this);
            }
            Debug.Log("Stock Notify( ) called");
        }

        // Gets or sets the price
        public double Price
        {
            get { return _price; }
            set
            {
                if (_price != value)
                {
                    _price = value;
                    Notify();
                }
            }
        }

        // Gets the symbol
        public string Symbol
        {
            get { return _symbol; }
        }
    }

    /// <summary>
    /// The 'ConcreteSubject' class
    /// 具体主题
    /// </summary>
    class IBM : Stock
    {
        // Constructor
        public IBM(string symbol, double price)
          : base(symbol, price)
        {
        }
    }

    /// <summary>
    /// The 'Observer' interface
    /// 投资者
    /// </summary>
    interface IInvestor
    {
        void Update(Stock stock);
    }

    /// <summary>
    /// The 'ConcreteObserver' class
    /// 具体观察者
    /// </summary>
    class Investor : IInvestor
    {
        private string _name;
        private Stock _stock;

        // Constructor
        public Investor(string name)
        {
            this._name = name;
        }

        public void Update(Stock stock)
        {
            //Debug.Log("Notified {0} of {1}'s " +"change to {2:C}", _name, stock.Symbol, stock.Price);
            Debug.Log("Notified "+ _name+" of "+ stock+"'s " + "change to "+stock.Price);
        }

        // Gets or sets the stock
        public Stock Stock
        {
            get { return _stock; }
            set { _stock = value; }
        }
    }
}
