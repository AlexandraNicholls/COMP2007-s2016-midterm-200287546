﻿<%@ Page Title="Todo Details" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TodoDetails.aspx.cs" Inherits="COMP2007_S2016_MidTerm1_200287546.TodoDetails" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row">
            <div class=" col-md-8">
                <h1>Create a New Todo Item</h1>
                <div class="form-group">
                    <label class="control-label" for="TodoNameTextBox">Name: </label>
                    <asp:TextBox runat="server" CssClass="form-control" ID="TodoNameTextBox" placeholder="Name your item." required="true"></asp:TextBox>
                    <asp:RequiredFieldValidator Display="Dynamic" CssClass="alert-danger" ID="RequiredFieldValidator1" runat="server" ErrorMessage="You need to input a name!" ControlToValidate="TodoNameTextBox"></asp:RequiredFieldValidator>
                </div>
                <div class="form-group">
                    <label class="control-label" for="TodoNotesTextBox">Notes: </label>
                    <asp:TextBox ID="TodoNotesTextBox" runat="server" TextMode="MultiLine" Columns="3" Rows="3" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="checkbox">
                    <label>
                        <input id="CompletedCheckBox" runat="server" type="checkbox" value="">Completed
                    </label>
                </div>
                <div class="text-right">
                    <asp:Button Text="Save" runat="server" CssClass="btn btn-primary btn-lg" ID="SaveButton" OnClick="SaveButton_Click" />
                    <asp:Button Text="Cancel" runat="server" CssClass="btn btn-warning btn-lg" ID="CancelButton" CausesValidation="false" UseSubmitBehavior="false" OnClick="CancelButton_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
