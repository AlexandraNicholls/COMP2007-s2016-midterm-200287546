using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

// required for EF DB access
using COMP2007_S2016_MidTerm1_200287546.Models;
using System.Web.ModelBinding;
using System.Linq.Dynamic;

/*
*@File Name: TodoList.aspx.cs
*@Author: Alex Nicholls
*@Student Number: 200287546
*@Site: Midterm
*@Date: June 23, 2016
*@Description: This page displays a list of todo items.  
*/

namespace COMP2007_S2016_MidTerm1_200287546
{
    public partial class TodoList : System.Web.UI.Page
    {
        /**
         * <summary>
         * This method checks if this page has been loaded before
         * </summary>
         * 
         * @method Page_Load
         * @returns {void}
         */
        protected void Page_Load(object sender, EventArgs e)
        {
            //check for postback
            if (!IsPostBack)
            {
                GetTodos();
                //add the most recent todo item
            }//end of postback check
        }//end of page load

        /**
        * <summary>
        * This method gets the Todo list data from the DB
        * </summary>
        * 
        * @method GetTodos
        * @returns {void}
        */
        protected void GetTodos()
        {
            // connect to EF
            using (TodoConnection db = new TodoConnection())
            {
                // query the users table using EF and LINQ
                var Todos = (from allTodos in db.Todos
                             select allTodos);

                //count the list items
                var count = (from allTodos in db.Todos
                             select allTodos).Count();

                CountLabel.Text = count.ToString();

                //bind the result to the GridView
                TodoGridView.DataSource = Todos.ToList();
                TodoGridView.DataBind();
            }

        }//end of get games

        protected void TodoGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // store which row was clicked
            int selectedRow = e.RowIndex;

            // get the selected ID using the Grid's DataKey collection
            int TodoID = Convert.ToInt32(TodoGridView.DataKeys[selectedRow].Values["TodoID"]);

            // use EF to find the selected user in the DB and remove it
            using (TodoConnection db = new TodoConnection())
            {
                // create object of the user class and store the query string inside of it
                Todo deletedTodo = (from TodoList in db.Todos
                                    where TodoList.TodoID == TodoID
                                    select TodoList).FirstOrDefault();

                // remove from the db
                db.Todos.Remove(deletedTodo);

                // save changes 
                db.SaveChanges();

                // refresh the grid
                this.GetTodos();
            }
        }//end of deleting row
    }//end of class
}//end of file