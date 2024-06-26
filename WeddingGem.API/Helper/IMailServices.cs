using Demo_Dal.Entities;

namespace WeddingGem.API.Helper
{
    public interface IMailServices
    {
        void SendEmail(Email email);
    }
}
