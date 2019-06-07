using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace servis
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {
        [WebMethod]
        public Urunler UrunListesi(int id)
        {
            Urunler urunler = new Urunler();
            string cs = ConfigurationManager.ConnectionStrings["DBBaglan"].ConnectionString;
            using (SqlConnection con=new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("URUN", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter parameter = new SqlParameter();
                parameter.ParameterName = "@id";
                parameter.Value = id;

                cmd.Parameters.Add(parameter);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    urunler.id = Convert.ToInt32(dr["id"]);
                    urunler.urunadi = dr["urunadi"].ToString();
                    urunler.aciklama = dr["aciklama"].ToString();
                }
            }
            return urunler;
        }
    }
}
