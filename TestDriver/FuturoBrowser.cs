using Common;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace TestDriver;

public class FuturoBrowser : HttpBrowserBase {

    public void maximize() {
        driver.Manage().Window.Maximize();
    }
    public void gotoFrontendHome() {
        gotoUrl("https://fas-testing.ec-giken.com/");
    }
    public bool isFrontendLoggedIn() {
        var buttons = driver.FindElements(By.XPath("//*[@id=\"html-body\"]/div[2]/header/div[1]/div/ul[1]/li[1]/a"));
        if (buttons.Count > 0) {
            var button = buttons[0];
            if (button.Text.Equals("ログイン")) {
                return false;
            }
        }
        return true;
    }
    public void loginFrontend() {
        if (isFrontendLoggedIn()) {
            return;
        }
        gotoUrl("https://fas-testing.ec-giken.com/japanese/customer/account/login/");
        var email = driver.FindElement(By.Id("email"));
        SimpleSendKeys(email, user);
        var password = driver.FindElement(By.Id("pass"));
        SimpleSendKeys(password, this.password);
        SimpleClick(By.Id("send2"));
    }
    public void logoutFrontend() {
        if (isFrontendLoggedIn()) {
            SimpleClick(By.XPath("//*[@id=\"html-body\"]/div[2]/header/div[1]/div/ul[1]/li[3]/a"));
        }
    }
    private string user = "hosokawa.tetsuichi+001@gmail.com";
    private string password = "abcABC123!#$";
    public void createFrontendAccount() {
        logoutFrontend();
        gotoUrl("https://fas-testing.ec-giken.com/japanese/customer/account/create/");
        var firstname = driver.FindElement(By.Id("firstname"));
        SimpleSendKeys(firstname, "哲一");
        var lastname = driver.FindElement(By.Id("lastname"));
        SimpleSendKeys(lastname, "細川");
        var email = driver.FindElement(By.Id("email_address"));
        SimpleSendKeys(email, user);
        var password = driver.FindElement(By.Id("password"));
        SimpleSendKeys(password, this.password);
        var password_confirmation = driver.FindElement(By.Id("password-confirmation"));
        SimpleSendKeys(password_confirmation, this.password);
        SimpleClick(By.XPath("//*[@id=\"form-validate\"]/div/div[1]/button"));
    }

    public void createFrontendAddress() {
        loginFrontend();
        gotoUrl("https://fas-testing.ec-giken.com/japanese/customer/address/new/");
        var firstname = driver.FindElement(By.Id("firstname"));
        SimpleSendKeys(firstname, "哲一");
        var lastname = driver.FindElement(By.Id("lastname"));
        SimpleSendKeys(lastname, "細川");
        var telephone = driver.FindElement(By.Id("telephone"));
        SimpleSendKeys(telephone, "08084659507");
        var street_1 = driver.FindElement(By.Id("street_1"));
        SimpleSendKeys(street_1, "2-10-17-1");
        SelectElement region_id = new SelectElement(driver.FindElement(By.Id("region_id")));
        region_id.SelectByText("奈良県");
        var city = driver.FindElement(By.Id("city"));
        SimpleSendKeys(city, "北葛城郡上牧町米山台");
        var zip = driver.FindElement(By.Id("zip"));
        SimpleSendKeys(zip, "6390213");
        SimpleWait();
        SimpleClickIfExists(By.Id("primary_billing"));
        SimpleClickIfExists(By.Id("primary_shipping"));
        SimpleClick(By.XPath("//*[@id=\"form-validate\"]/div/div[1]/button"));
    }
    public int buyProduct() {
        gotoUrl("https://fas-testing.ec-giken.com/japanese/testtest");
        SimpleClick(By.XPath("//*[@id=\"product-options-wrapper\"]/div/div/div/div/div[2]/div/div/div[2]/div/button"));
        SimpleClick(By.XPath("//*[@id=\"maincontent\"]/div[2]/div/div[3]/div[1]/ul/li[1]/button"));
        SimpleClick(By.XPath("//*[@id=\"shipping-method-buttons-container\"]/div/button"));
        SimpleClick(By.XPath("//*[@id=\"paygent_cc\"]"));
        var paygent_cc_cc_number = driver.FindElement(By.Id("paygent_cc_cc_number"));
        SimpleSendKeys(paygent_cc_cc_number, "4111111111111111");
        SelectElement paygent_cc_expiration = new SelectElement(driver.FindElement(By.Id("paygent_cc_expiration")));
        paygent_cc_expiration.SelectByValue("1");
        SelectElement paygent_cc_expiration_yr = new SelectElement(driver.FindElement(By.Id("paygent_cc_expiration_yr")));
        paygent_cc_expiration_yr.SelectByValue("2030");
        var paygent_cc_cc_cid = driver.FindElement(By.Id("paygent_cc_cc_cid"));
        SimpleSendKeys(paygent_cc_cc_cid, "123");
        SimpleClick(By.Id("paygent_cc_agree"));
        SimpleClick(By.XPath("//*[@id=\"checkout-payment-method-load\"]/div/div/div[2]/div[3]/div[2]/div/button"));
        var increment_id = WaitBy(By.XPath("//*[@id=\"maincontent\"]/div[2]/div/div[3]/p[1]/a/strong"));
        int value = Int32.Parse(increment_id.Text);
        return value;
    }
    public int buyProductGuest() {
        logoutFrontend();
        gotoUrl("https://fas-testing.ec-giken.com/japanese/testtest");
        SimpleClick(By.XPath("//*[@id=\"product-options-wrapper\"]/div/div/div/div/div[2]/div/div/div[2]/div/button"));
        SimpleClick(By.XPath("//*[@id=\"maincontent\"]/div[2]/div/div[3]/div[1]/ul/li[1]/button"));
        var customer_email = WaitBy(By.Id("customer-email"));
        SimpleSendKeys(customer_email, user);
        var zip = WaitBy(By.XPath("//input[@name=\"postcode\"]"));
        SimpleSendKeys(zip, "6390213");
        var region_id = new SelectElement(driver.FindElement(By.XPath("//select[@name=\"region_id\"]")));
        region_id.SelectByText("奈良県");
        var city = driver.FindElement(By.XPath("//input[@name=\"city\"]"));
        SimpleSendKeys(city, "北葛城郡上牧町米山台");
        var street = driver.FindElement(By.XPath("//input[@name=\"street[0]\"]"));
        SimpleSendKeys(street, "2-10-17-1");
        var lastname = driver.FindElement(By.XPath("//input[@name=\"lastname\"]"));
        SimpleSendKeys(lastname, "細川");
        var firstname = driver.FindElement(By.XPath("//input[@name=\"firstname\"]"));
        SimpleSendKeys(firstname, "哲一");
        var telephone = driver.FindElement(By.XPath("//input[@name=\"telephone\"]"));
        SimpleSendKeys(telephone, "080-8465-9507");
        var vw_delivery_comment = driver.FindElement(By.Id("vw_delivery_comment"));
        SimpleSendKeys(vw_delivery_comment, "テスト注文です。\r\n無視してください。");
        SimpleClick(By.XPath("//*[@id=\"shipping-method-buttons-container\"]/div/button"));
        SimpleClick(By.XPath("//*[@id=\"paygent_cc\"]"));
        var paygent_cc_cc_number = driver.FindElement(By.Id("paygent_cc_cc_number"));
        SimpleSendKeys(paygent_cc_cc_number, "4111111111111111");
        SelectElement paygent_cc_expiration = new SelectElement(driver.FindElement(By.Id("paygent_cc_expiration")));
        paygent_cc_expiration.SelectByValue("1");
        SelectElement paygent_cc_expiration_yr = new SelectElement(driver.FindElement(By.Id("paygent_cc_expiration_yr")));
        paygent_cc_expiration_yr.SelectByValue("2030");
        var paygent_cc_cc_cid = driver.FindElement(By.Id("paygent_cc_cc_cid"));
        SimpleSendKeys(paygent_cc_cc_cid, "123");
        SimpleClick(By.Id("paygent_cc_agree"));
        SimpleClick(By.XPath("//*[@id=\"checkout-payment-method-load\"]/div/div/div[2]/div[3]/div[2]/div/button"));
        var increment_id = WaitBy(By.XPath("//*[@id=\"maincontent\"]/div[2]/div/div[3]/p[1]/span"));
        int value = Int32.Parse(increment_id.Text);
        return value;
    }
    public int buyProductWithLogin() {
        gotoUrl("https://fas-testing.ec-giken.com/japanese/testtest");
        SimpleClick(By.XPath("//*[@id=\"product-options-wrapper\"]/div/div/div/div/div[2]/div/div/div[2]/div/button"));
        SimpleClick(By.XPath("//*[@id=\"maincontent\"]/div[2]/div/div[3]/div[1]/ul/li[1]/button"));
        var customer_account = WaitBy(By.Id("customer-account"));
        SimpleSendKeys(customer_account, user);
        var customer_password = driver.FindElement(By.Id("customer-password"));
        SimpleSendKeys(customer_password, this.password);
        SimpleClick(By.XPath("//*[@id=\"customer-account-fieldset\"]/fieldset/div[2]/div[1]/button"));
        SimpleClick(By.XPath("//*[@id=\"shipping-method-buttons-container\"]/div/button"));
        SimpleClick(By.XPath("//*[@id=\"paygent_cc\"]"));
        var paygent_cc_cc_number = driver.FindElement(By.Id("paygent_cc_cc_number"));
        SimpleSendKeys(paygent_cc_cc_number, "4111111111111111");
        SelectElement paygent_cc_expiration = new SelectElement(driver.FindElement(By.Id("paygent_cc_expiration")));
        paygent_cc_expiration.SelectByValue("1");
        SelectElement paygent_cc_expiration_yr = new SelectElement(driver.FindElement(By.Id("paygent_cc_expiration_yr")));
        paygent_cc_expiration_yr.SelectByValue("2030");
        var paygent_cc_cc_cid = driver.FindElement(By.Id("paygent_cc_cc_cid"));
        SimpleSendKeys(paygent_cc_cc_cid, "123");
        SimpleClick(By.Id("paygent_cc_agree"));
        SimpleClick(By.XPath("//*[@id=\"checkout-payment-method-load\"]/div/div/div[2]/div[3]/div[2]/div/button"));
        var increment_id = WaitBy(By.XPath("//*[@id=\"maincontent\"]/div[2]/div/div[3]/p[1]/a/strong"));
        int value = Int32.Parse(increment_id.Text);
        return value;
    }
}
