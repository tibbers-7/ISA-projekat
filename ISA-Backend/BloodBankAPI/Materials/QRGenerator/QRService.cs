using QRCoder;

namespace BloodBankAPI.Materials.QRGenerator
{
    public class QRService : IQRService
    {
        public byte[] GenerateQR(string data,string fileName)
        {
            QRCodeGenerator _qrCode = new QRCodeGenerator();
            QRCodeData _qrCodeData = _qrCode.CreateQrCode(data, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(_qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);
            saveToFile(qrCodeImage,fileName);
            return BitmapToBytesCode(qrCodeImage);

        }

        private static Byte[] BitmapToBytesCode(Bitmap image)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                image.Save(stream, ImageFormat.Png);
                return stream.ToArray();
            }
        }

        private void saveToFile(Bitmap bitmap,string fileName)
        {
            string filepath = "AppData/"+fileName;
            bitmap.Save(filepath, ImageFormat.Jpeg);
            
        }

    }
}
