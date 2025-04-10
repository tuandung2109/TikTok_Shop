using Newtonsoft.Json;
using ThuongMaiDienTu.Models;

namespace ThuongMaiDienTu.Helpers
{
    public class CartHelper
    {
        private const string CartSessionKey = "Cart";

        public static void SaveCart(ISession session, List<SanPham> sanPham)
        {
            session.SetString(CartSessionKey, JsonConvert.SerializeObject(sanPham));
        }

        public static List<SanPham> GetCart(ISession session) {
            var cartData = session.GetString(CartSessionKey);
            return cartData == null ? new List<SanPham>() : JsonConvert.DeserializeObject < List<SanPham> >(cartData);
        }

        public static int GetCartCount(ISession session)
        {
            var cartData = CartHelper.GetCart(session);
            return cartData.Count; // Chỉ trả về số lượng mặt hàng (số phần tử trong danh sách)
        }

        public static decimal GetCartPrice(ISession session) {
            var carData = CartHelper.GetCart(session);
            var price = 0;
            foreach(var i in carData)
            {
                price += (int) i.Gia_Khuyen_Mai * i.So_Luong_Ton;
            }
            return price;
        }
    }
}
