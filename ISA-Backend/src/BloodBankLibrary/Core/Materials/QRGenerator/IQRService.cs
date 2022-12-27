using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodBankLibrary.Core.Materials.QRGenerator
{
    public interface IQRService
    {
        public Byte[] GenerateQR(string data,string fileName);
    }
}
