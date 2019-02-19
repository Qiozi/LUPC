<%@LANGUAGE="VBSCRIPT" CODEPAGE="65001"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>Untitled Document</title>
<script type="text/javascript">
<!--
function MM_jumpMenu(targ,selObj,restore){ //v3.0
  eval(targ+".location='"+selObj.options[selObj.selectedIndex].value+"'");
  if (restore) selObj.selectedIndex=0;
}
//-->
</script>
</head>

<body>
<form id="form1" name="form1" method="post" action="../paypal_test/send_to_paypal.asp">
  <table width="200" border="1">
<!--    <tr>
      <td colspan="2">Shipping address</td>
    </tr>
    <tr>
      <td>Fast Name</td>
      <td><label>
        <input type="text" name="firat_name" id="firat_name" tabindex="1" />
      </label></td>
    </tr>
    <tr>
      <td>Last Name</td>
      <td><label>
        <input type="text" name="last_name" id="last_name" tabindex="2" />
      </label></td>
    </tr>
    <tr>
      <td>Street Address1</td>
      <td><label>
        <input type="text" name="street_address1" id="street_address1" tabindex="3" />
      </label></td>
    </tr>
    <tr>
      <td>Street Address2</td>
      <td><label>
        <input type="text" name="street_address2" id="street_address2" tabindex="4" />
      </label></td>
    </tr>
    <tr>
      <td>City</td>
      <td><label>
        <input type="text" name="city" id="city" tabindex="5" />
      </label></td>
    </tr>
    <tr>
      <td>State</td>
      <td><select name="state" id="state">
        <option>1</option>
      </select></td>
    </tr>
    <tr>
      <td>Zip/Postal Code</td>
      <td><label>
        <input type="text" name="zip_code" id="zip_code" tabindex="7" />
      </label></td>
    </tr>
    <tr>
      <td>Country</td>
      <td><select name="country" id="country">
        <option>1</option>
      </select></td>
    </tr>
    <tr>
      <td>Telephone Number</td>
      <td><label>
        <input type="text" name="telephone_number" id="telephone_number" tabindex="9" />
      </label></td>
    </tr>
    <tr>
      <td colspan="2">Shipping Method</td>
    </tr>
    <tr>
      <td>Shipping Method</td>
      <td><select name="shipping_method" id="shipping_method">
        <option>1</option>
      </select></td>
    </tr>-->
    <tr>
      <td>&nbsp;</td>
      <td><label>
        <input type="submit" name="button" id="button" value="Submit" />
      </label></td>
    </tr>
  </table>
  <input type="hidden" name="user" value="qiozi4_1223833183_biz@163.com" />
  <input type="hidden" name="pwd" value="1234qwer" />
  <input type="hidden" name="method" value="SetExpressCheckout" />
  <input type="hidden" name="returnurl" value="http://localhost/paypal_test/return_ok.asp" />
  <input type="hidden" name="cancelurl" value="http://localhost/paypal_test/return_cancel.asp" />
  <input type="hidden" name="PAYMENTACTION" value="Sale" />
  	
</form>
</body>
</html>
