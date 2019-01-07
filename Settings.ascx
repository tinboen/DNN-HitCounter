<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Settings.ascx.cs" Inherits="DotNetNuke.Modules.HitCounter.Settings" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>

<div class="container">
    <div class="dnnFormItem">
        <dnn:Label id="lblNoOfDigits" runat="server" ResourceKey="lblNoOfDigits" Text="No of Digits" />
        <asp:TextBox ID="txtNoOfDigits" runat="server" width="60px" />
    </div>
    <div class="dnnFormItem">
        <dnn:Label id="lblCounterType" runat="server" ResourceKey="lblCounterType" Text="Counter Type" />
        <asp:DropDownList ID="ddCounterType" runat="server" width="120px" />
    </div>
</div>
