using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using _016_AngularJs_Mvc_Demo.Models;

namespace _016_AngularJs_Mvc_Demo.Classes
{
    public class UserDALcls
    {

        string strConnection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString.ToString();

        public void insert(UserModels userModelObj) 
        {
            using (SqlConnection con = new SqlConnection(strConnection))
            {
                SqlCommand cmd = new SqlCommand("insert into TblMvcUser(Fname,Lname,Uname,Email,MobileNo,Password,Role) values('" + userModelObj.FName + "','" + userModelObj.LName + "','" + userModelObj.UName + "','" + userModelObj.Email + "','" + userModelObj.MobileNo + "','" + userModelObj.Password + "','" + userModelObj.Role + "')", con);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<UserModels> getList()
        {
            List<UserModels> _lstPoducts = new List<UserModels>();
            SqlConnection con = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand("select * from TblMvcUser order By UserID asc", con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {

                UserModels _Products = new UserModels();

                
                _Products.UserID = Convert.ToInt16(dr["UserID"].ToString());
                _Products.FName = dr["FName"].ToString();
                _Products.LName = dr["LName"].ToString();
                _Products.UName = dr["UName"].ToString();
                _Products.Password = dr["Password"].ToString();
                _Products.Email = dr["Email"].ToString();
                _Products.MobileNo = dr["MobileNo"].ToString();
                _Products.Role = dr["Role"].ToString();
                _Products.ImageName = dr["ImageName"].ToString();

                _lstPoducts.Add(_Products);


            }

           

            return _lstPoducts;
        }

        public List<UserModels> getSearchList(UserModels userModelObj)
        {
            List<UserModels> _lstPoducts = new List<UserModels>();
            SqlConnection con = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand("select * from TblMvcUser WHERE Fname LIKE '%" + userModelObj.SearchString + "%'", con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();



            while (dr.Read())
            {

                UserModels _Products = new UserModels();


                _Products.UserID = Convert.ToInt16(dr["UserID"].ToString());
                _Products.FName = dr["FName"].ToString();
                _Products.LName = dr["LName"].ToString();
                _Products.UName = dr["UName"].ToString();
                _Products.Password = dr["Password"].ToString();
                _Products.Email = dr["Email"].ToString();
                _Products.MobileNo = dr["MobileNo"].ToString();
                _Products.Role = dr["Role"].ToString();

                _lstPoducts.Add(_Products);


            }



            return _lstPoducts;
        }

        public UserModels getListByID(UserModels userModelObjEdit)
        {
            
            SqlConnection con = new SqlConnection(strConnection);
            SqlCommand cmd1 = new SqlCommand("select * from TblMvcUser where UserID=" + userModelObjEdit.UserID, con);
           
            con.Open();
            SqlDataReader dr1 = cmd1.ExecuteReader();

            UserModels _Products = new UserModels();

            if (dr1.Read())
            {

               
                _Products.UserID = Convert.ToInt16(dr1["UserID"].ToString());
                _Products.FName = dr1["Fname"].ToString();
                _Products.LName = dr1["Lname"].ToString();
                _Products.UName = dr1["Uname"].ToString();
                _Products.Email = dr1["Email"].ToString();
                _Products.MobileNo = dr1["MobileNo"].ToString();
                _Products.Password = dr1["Password"].ToString();
                _Products.Role = dr1["Role"].ToString();


            }
                return _Products;
        }

        public void update(UserModels userModelObjEdit)
        {

            using (SqlConnection con = new SqlConnection(strConnection))
            {

                string str_query = "update TblMvcUser SET Fname ='" + userModelObjEdit.FName + "' , Lname ='" + userModelObjEdit.LName + "' , Uname ='" + userModelObjEdit.UName + "' , Email ='" + userModelObjEdit.Email + "' , MobileNo ='" + userModelObjEdit.MobileNo + "' , Role ='" + userModelObjEdit.Role + "' Where UserID=" + userModelObjEdit.UserID;

                SqlCommand cmd = new SqlCommand("" + str_query, con);
                
                con.Open();
                cmd.ExecuteNonQuery();

            }
        }

        public void delete(UserModels userModelObj) 
        {
            SqlConnection con = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand("delete from TblMvcUser where UserID=" + userModelObj.UserID, con);
            con.Open();
            cmd.ExecuteNonQuery();
        }

    }
}