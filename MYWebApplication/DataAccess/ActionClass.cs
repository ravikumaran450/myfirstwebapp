using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using MYWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MYWebApplication.DataAccess
{
    public class ActionClass
    {
        string ConnectingString = ConnectionString.CName;
       

        public void AddEmployee(Employee employee)
        {
            var emplist = GetAllEmployee().ToList();
            int empcount = emplist.Count;
            int empidMax = emplist.Max(e => e.EmpID);
            int empId;
            if(empcount < empidMax)
            {
                empId = empidMax + 1;
            }
            else
            {
                empId = empcount + 1;
            }
            using (SqlConnection con = new SqlConnection(ConnectingString))
            {
                SqlCommand cmd = new SqlCommand("spAddEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                                
                cmd.Parameters.AddWithValue("@EmpID", empId);
                cmd.Parameters.AddWithValue("@CompanyName", employee.ProjectName);
                cmd.Parameters.AddWithValue("@EmployeeName", employee.ProjectManagerName);
                cmd.Parameters.AddWithValue("@Designation", employee.EmpDesignation);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

            }
        }
       public void UpdateEmployee(int empid,Employee employee)
        {
         
            
                using (SqlConnection con = new SqlConnection(ConnectingString))
                {
                    SqlCommand cmd = new SqlCommand("UpdateEmployee", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmpID", employee.EmpID);
                    cmd.Parameters.AddWithValue("@CompanyName", employee.ProjectName);
                    cmd.Parameters.AddWithValue("@EmployeeName", employee.ProjectManagerName);
                    cmd.Parameters.AddWithValue("@Designation", employee.EmpDesignation);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();


                }
         

        }
            public IEnumerable<Employee> GetAllEmployee()
            {
            List<Employee> lstEmployee = new List<Employee>();
            using (SqlConnection con = new SqlConnection(ConnectingString))
            {
                SqlCommand cmd = new SqlCommand("spGetAllEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Employee employee = new Employee();
                   
                    employee.ProjectName = rdr["CompanyName"].ToString();
                    employee.ProjectManagerName = rdr["EmployeeName"].ToString();
                    employee.EmpDesignation = rdr["Designation"].ToString();
                    employee.EmpID = Convert.ToInt32(rdr["EmpID"]);
                    
                    lstEmployee.Add(employee);
                }
                con.Close();
            }
            return lstEmployee;
        }

        internal Employee GetAllEmployeeById(int empid)
        {
            Employee employee = new Employee();

            using (SqlConnection con = new SqlConnection(ConnectingString))
            {

                SqlCommand cmd = new SqlCommand("spGetAllEmployeeByID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmpID", empid);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    employee.EmpID = Convert.ToInt32(rdr["EmpID"]);
                    employee.ProjectName = rdr["CompanyName"].ToString();
                    employee.ProjectManagerName = rdr["EmployeeName"].ToString();
                    employee.EmpDesignation = rdr["Designation"].ToString();

                }
                con.Close();
            }
            return employee;
        }

        
        public void DeleteEmployee(int empid,Employee employee)
        {
            using(SqlConnection con=new SqlConnection(ConnectingString))
            {
                SqlCommand cmd = new SqlCommand("DeleteEmp", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmpID", employee.EmpID);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

            }
        }

        public LoginUser Login(LoginUser u)
        {
            using (EmployeeProjectContext db = new EmployeeProjectContext())
            {
               LoginUser obj = db.LoginUser.Where(a => a.UserName.Equals(u.UserName) && a.Passward.Equals(u.Passward)).FirstOrDefault();
                if (obj != null)
                {
                    return obj;
                }

            }
            return null;
        }
    
}
}
