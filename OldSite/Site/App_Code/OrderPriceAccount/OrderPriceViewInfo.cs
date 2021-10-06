using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for OrderPriceViewInfo
/// </summary>
[Serializable]
public class OrderPriceViewInfo
{
    public OrderPriceViewInfo()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public decimal PartDiscount { get; set; }

    decimal _subTotal = 0M;

    public decimal SubTotal
    {
        get { return _subTotal; }
        set { _subTotal = value; }
    }
    decimal _specialCashDiscount = 0M;

    public decimal SpecialCashDiscount
    {
        get { return _specialCashDiscount; }
        set { _specialCashDiscount = value; }
    }
    decimal _inputDiscount = 0M;

    public decimal InputDiscount
    {
        get { return _inputDiscount; }
        set { _inputDiscount = value; }
    }
    decimal _inputShipCharge = 0M;

    public decimal InputShipCharge
    {
        get { return _inputShipCharge; }
        set { _inputShipCharge = value; }
    }
    decimal _shipCharge = 0M;

    public decimal ShipCharge
    {
        get { return _shipCharge; }
        set { _shipCharge = value; }
    }
    decimal _taxableTotal = 0M;

    public decimal TaxableTotal
    {
        get { return _taxableTotal; }
        set { _taxableTotal = value; }
    }
    decimal _salesTax = 0M;

    public decimal SalesTax
    {
        get { return _salesTax; }
        set { _salesTax = value; }
    }
    decimal _weee = 0M;

    public decimal Weee
    {
        get { return _weee; }
        set { _weee = value; }
    }
    decimal _grandTotal = 0M;

    public decimal GrandTotal
    {
        get { return _grandTotal; }
        set { _grandTotal = value; }
    }
    decimal _taxPercent = 0M;

    public decimal TaxPercent
    {
        get { return _taxPercent; }
        set { _taxPercent = value; }
    }

    string _priceUnit = "";

    public string PriceUnit
    {
        get { return _priceUnit; }
        set { _priceUnit = value; }
    }

    decimal _hst = 0M;

    public decimal Hst
    {
        get { return _hst; }
        set { _hst = value; }
    }
    decimal _gst = 0M;

    public decimal Gst
    {
        get { return _gst; }
        set { _gst = value; }
    }
    decimal _pst = 0M;

    public decimal Pst
    {
        get { return _pst; }
        set { _pst = value; }
    }
    decimal _hst_rate = 0M;

    public decimal Hst_rate
    {
        get { return _hst_rate; }
        set { _hst_rate = value; }
    }
    decimal _gst_rate = 0M;

    public decimal Gst_rate
    {
        get { return _gst_rate; }
        set { _gst_rate = value; }
    }
    decimal _pst_rate = 0M;

    public decimal Pst_rate
    {
        get { return _pst_rate; }
        set { _pst_rate = value; }
    }
}