using APIDemo.Models;
using DataAccess;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace APIDemo.Controllers
{
    [APIBasicAuthentication]//there are note in bottom 
    [EnableCors("*","*","*")]
    public class EmployeeServiceController : ApiController
    {
        [HttpGet]
        public List<Employee> Get()
        {
            DataAccessContext context = new DataAccessContext();
            return context.Employees.ToList();
        }
        [HttpGet]
        public HttpResponseMessage GetEmployeeBasedId(int? Id)
        {
            try
            {
                DataAccessContext context = new DataAccessContext();
                Employee employee = context.Employees.SingleOrDefault(emp => emp.ID == Id);
                return Request.CreateResponse(HttpStatusCode.OK, employee);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
        [HttpPost]
        public HttpResponseMessage Post(Employee employee) 
        {
            try
            {
                DataAccessContext context = new DataAccessContext();
                context.Employees.Add(employee);
                context.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.Created, employee);
            }
            catch (Exception ex) 
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,ex);
            }
        }
        [HttpPut]
        public HttpResponseMessage Put(Employee employee) 
        {
            try
            {
                DataAccessContext context = new DataAccessContext();
                Employee emp = context.Employees.SingleOrDefault(e => e.ID == employee.ID);
                emp.FirstName = employee.FirstName;
                emp.LastName = employee.LastName;
                emp.Gender = employee.Gender;
                emp.Salary = employee.Salary;

                context.Entry(emp).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, employee);
            }
            catch (Exception ex) 
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        [HttpDelete]
        public HttpResponseMessage DeleteEmployee(int Id) 
        {
            try
            {
                DataAccessContext context = new DataAccessContext();
                Employee employee = context.Employees.SingleOrDefault(emp => emp.ID == Id);
                context.Employees.Remove(employee);
                context.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex) 
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,ex);
            }
        }
    }
}

/*
 * why we need to use [APIBasicAuthentication] on this controller
 * although we prevent user to call api if he/she not login
 * because you can call api from anywhere you want so if you not
 * add [APIBasicAuthentication] on this controller and there are
 * system not have login page so the user will be have able to call
 * api although he don't have authentication also if you don't
 * add [APIBasicAuthentication] on this controller then enter api link
 * in url so in this case you will be able to call api but if
 * i add [APIBasicAuthentication] => i will force any person want
 * to call api should be send userName & passwrod in header request
 * to be able to call api
 */