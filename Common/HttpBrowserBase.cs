namespace Common;

using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.IO;
using System.Reflection;
using System.Threading;
public class HttpBrowserBase
{
    protected ChromeDriver driver;
    protected const int sleep_time = 1000;
    public HttpBrowserBase() {
        Assembly? current = Assembly.GetExecutingAssembly();
        if (current != null) {
            driver = new ChromeDriver(Path.GetDirectoryName(current.Location));
        } else {
            throw new Exception("Chrome Driver cannot initialize.");
        }
    }
    public void gotoUrl(string url) {
        driver.Url = url;
    }
    public void SimpleWait() {
        Thread.Sleep(sleep_time);
    }
    public void quit() {
        driver.Quit();
    }
    public IWebElement WaitBy(By by) {
        WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 0, 30));
        var elem = wait.Until(ExpectedConditions.ElementIsVisible(by));
        return elem;
    }
    public void SimpleClick(By by) {
        WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 0, 30));
        var clickable = wait.Until(ExpectedConditions.ElementIsVisible(by));
        string script = "window.scrollTo(0, " + clickable.Location.Y + ");";
        driver.ExecuteScript(script);
        SimpleWait();
        for (var i = 0; i < 10; i++) {
            try {
                clickable.Click();
                break;
            } catch (ElementClickInterceptedException e) {
                SimpleWait();
            }
        }
    }
    public void SimpleClickIfExists(By by) {
        try {
            WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 0, 5));
            var clickable = wait.Until(ExpectedConditions.ElementIsVisible(by));
            string script = "window.scrollTo(0, " + clickable.Location.Y + ");";
            driver.ExecuteScript(script);
            SimpleWait();
            clickable.Click();
        } catch (Exception e) {
            // nothing to do
        }
    }
    public void SimpleSendKeys(IWebElement input, string value) {
        input.SendKeys(Keys.Control + "a");
        input.SendKeys(value);
    }
}
