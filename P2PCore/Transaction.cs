using System;
using System.Collections.Generic;
using System.Text;

namespace P2PCore
{
    public class Transaction
    {
        public string FromAddress { get; set; }
        public string ToAddress { get; set; }
        public int Amount { get; set; }

        public Transaction(string fromAddress, string toAddress, int amount)
        {
            FromAddress = fromAddress;
            ToAddress = toAddress;
            Amount = amount;
        }
    }
}
