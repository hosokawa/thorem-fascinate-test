namespace TestDriver;
class TffTicket64 {
    protected FuturoBrowser browser;

    public TffTicket64() {
        browser = new FuturoBrowser();
    }

    public int execute() {
        browser.maximize();
        browser.gotoFrontendHome();
        browser.createFrontendAccount();
        browser.createFrontendAddress();
        return browser.buyProduct();
    }

    public int executeGuest() {
        return browser.buyProductGuest();
    }
    public int executeWithLogin() {
        return browser.buyProductWithLogin();
    }
    ~TffTicket64() {
        browser.quit();
    }
}
