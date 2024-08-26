Imports System.Text
Imports NUnit.Framework
Imports OpenQA.Selenium
Imports OpenQA.Selenium.Chrome
Imports OpenQA.Selenium.Chrome.ChromeOptions
Imports OpenQA.Selenium.Firefox
Imports OpenQA.Selenium.Firefox.FirefoxProfileManager
Imports OpenQA.Selenium.Firefox.FirefoxProfile
Imports OpenQA.Selenium.Support.UI
Public Class WatsAppCls

    Private testContextInstance As TestContext
    'Const HomePageURL As String = "http://www.bing.com/"
    Const HomePageURL As String = "https://web.whatsapp.com/"
    'Const HomePageURL As String = "https://www.whatsapp.com//"
    Dim ProcessRunning As Integer

    '''<summary>
    '''Gets or sets the test context which provides
    '''information about and functionality for the current test run.
    '''</summary>
    Public Property TestContext() As TestContext
        Get
            Return testContextInstance
        End Get
        Set(ByVal value As TestContext)
            testContextInstance = value
        End Set
    End Property
    '<TestInitialize()>
    Public Sub Initialization(ByVal DriverOwner As IWebDriver)
        driver = DriverOwner
        'driver.Manage.Window.Size = New Size(5, 5)
        driver.Manage.Window.Minimize()
        'driver = New ChromeDriver

    End Sub
    Public Sub New(ByVal DriverOwner)
        Try
            ProcessRunning = 0
            Dim clsProcess As New Process   'create new instance of class process
            For Each clsProcess In Process.GetProcesses 'list all the processes
                If LCase(clsProcess.ProcessName) = LCase(DriverOwner) Then
                    ProcessRunning += 1
                ElseIf LCase(clsProcess.ProcessName) = "geckodriver" Then
                    ProcessRunning += 1
                End If
                If ProcessRunning > 1 Then Exit For
            Next
            If Not ProcessRunning > 1 Then
                Select Case LCase(DriverOwner)
                    Case LCase("Firefox")
                        Initialization(New FirefoxDriver)
                    Case LCase("Chrome")
                        Initialization(New ChromeDriver)
                End Select
            End If
        Catch ex As Exception
            ShowMsg(ex.Message, "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub

    '<TestCleanup()>
    Public Sub Termination()
        driver.Quit()
    End Sub
    '<TestMethod()>
    Public Sub TestMethod1()
        Try
            'ProcessRunning = False
            'Dim clsProcess As New Process   'create new instance of class process
            'For Each clsProcess In Process.GetProcesses 'list all the processes
            '    If clsProcess.ProcessName = "firefox" Then
            '        ProcessRunning += 1
            '    ElseIf clsProcess.ProcessName = "geckodriver" Then
            '        ProcessRunning += 1
            '    End If
            '    If ProcessRunning > 1 Then Exit For
            'Next

            'If Not ProcessRunning > 1 Then
            '    Initialization(driver)
            'End If
            'Go to Bing Homepage'
            If Not DirectCast(driver, OpenQA.Selenium.WebDriver).Url = "https://web.whatsapp.com/" Then
                driver.Manage.Window.Maximize()
                driver.Navigate.GoToUrl(HomePageURL)
                System.Threading.Thread.Sleep(500)
            End If


            Dim SearchBox As IWebElement
            Dim FirstResult As IWebElement
            Dim ExpectedText As String = String.Empty
            Try
RecheckIfLogedIn:
                FirstResult = GetWebElement(driver, By.XPath("/html/body/div[1]/div[1]/div/div[2]/div[1]/div/div[1]/div"), 10)
                ExpectedText = "To use WhatsApp on your computer:"
                Assert.AreEqual(ExpectedText, DirectCast(FirstResult, OpenQA.Selenium.WebElement).[Text], "Subject is not correct")
                If Sticky_Note_Form.Language_Btn.Text = "ع" Then
                    Msg = "You Are Not Loged Int To Whatsap Yet!!! To Send Message You Have To Log In First"
                Else
                    Msg = "لم تقم بتسجيل الدخول بعد على الواتساب!!! لارسال الرسالة يجب من تسجيل الدخول أولا"
                End If
                Dim MyDialogResult = ShowMsg(Msg & vbNewLine & "Sticky_Note_Setting.txt", "InfoSysMe (Stick Note)", MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2, MBOs, False)
                If MyDialogResult = DialogResult.Cancel Then Exit Sub
                System.Threading.Thread.Sleep(1500)
                GoTo RecheckIfLogedIn
            Catch ex As Exception
            End Try

            'Get Handle for Searchbox
            'SearchBox = مكان البحث فى الواتس على الاسم الذى تريد ارسال اليه
            SearchBox = GetWebElement(driver, By.XPath("/html/body/div[1]/div[1]/div[1]/div[3]/div/div[1]/div/label/div/div[2]"), 10)

            'Enter Search Text
            'ارسال الاسم للبحث عنه
            SearchBox.SendKeys("Magdy AlGamal")

            'Different ways to start the search
            'تفعيل عملية البحث
            SearchBox.SendKeys(Keys.Enter)

            Msg = Sticky_Note_Form.Sticky_Note_TxtBx.Text

            'البحث عن مكان كتابة الرسالة فى الواتس
            SearchBox = GetWebElement(driver, By.XPath("/html/body/div[1]/div[1]/div[1]/div[4]/div[1]/footer/div[1]/div/span[2]/div/div[2]/div[1]/div/div[2]"), 10)
            ' ارسال الرسالة الى مكان كتابة الرسالة فى الواتس
            Dim capabilities As ICapabilities = CType(driver, OpenQA.Selenium.WebDriver).Capabilities
            Dim BrowserName = capabilities.GetCapability("browserName")
            Select Case LCase(BrowserName)
                Case LCase("chrome")
                    Msg = Replace(Msg, vbLf, " -{[*]}- ")
                Case "firefox"
                    Msg = Replace(Msg, vbLf, " -{[🐧]}- ")
            End Select
            SearchBox.SendKeys(Msg)

            Dim Text As String
            Text = DirectCast(SearchBox, OpenQA.Selenium.WebElement).[Text]
            While Not String.IsNullOrEmpty(Text)
                Text = DirectCast(SearchBox, OpenQA.Selenium.WebElement).[Text]
                SearchBox.SendKeys(Keys.Enter)
                System.Threading.Thread.Sleep(1500)
            End While
            ' تفعيل ارسال الرسالة
            driver.Manage.Window.Minimize()
            If Sticky_Note_Form.Language_Btn.Text = "ع" Then
                Msg = "The Message Sent Successfully..."
            Else
                Msg = "تم إرسال الرسالة بنجاح..."
            End If
            ShowMsg(Msg, "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)



            ''Method 2: Grab Search-Button and click it
            ''Dim SearchButton As IWebElement
            ''SearchButton = GetWebElement(driver, By.Id("sb_form_go"), 10)
            ''SearchButton.Click()


            '''Now get the first result returned by Bing search
            ''FirstResult = GetWebElement(driver, By.XPath("//ul[@class='sb_results']/li/div/div/div/h1/a"), 10)
            ''/html/body/div[1]/main/ol/li[2]/h2/a
            ''<a href="https://www.dotnet-developer-conference.de/en/" h="ID=SERP,5150.1">DDC - .NET Developer Conference for .NET Developers ...</a>
            'FirstResult = GetWebElement(driver, By.XPath("/html/body/div[1]/main/ol/li[2]/h2/a"), 10)
            ''
            ''Method 1: Compare the subject
            ''Dim ExpectedText As String = "dotnet-developer.de | Tips for vb.net,…"
            'Dim ExpectedText As String = "DDC - .NET Developer Conference for .NET Developers ..."
            'Assert.AreEqual(ExpectedText, DirectCast(FirstResult, OpenQA.Selenium.WebElement).[Text], "Subject is not correct")


            ''Method 2: Compare the link
            ''Dim ExpectedURL As String = "http://www.dotnet-developer.de/"
            'Dim ExpectedURL As String = "https://www.dotnet-developer-conference.de/en/"
            'Assert.AreEqual(ExpectedURL, FirstResult.GetAttribute("href"), "URL is not correct!")
        Catch ex As Exception
            ShowMsg(ex.Message, "InfoSysMe (Stick Note)", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.ServiceNotification, False)
        End Try
    End Sub
    ''' <summary>
    ''' Retrieve Web Element using default driver and default timeout
    ''' </summary>
    ''' <param name="definition">Definition of the WebElement to grab</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Overloads Function GetWebElement(ByVal definition As OpenQA.Selenium.By) As IWebElement
        Const DefaultTimeout As Integer = 10
        Return GetWebelement(definition, DefaultTimeout)
    End Function
    ''' <summary>
    ''' Retrieve Web Element using default driver
    ''' </summary>
    ''' <param name="definition">Definition of the WebElement to grab</param>
    ''' <param name="timeoutSeconds">Seconds to wait until a timeout is thrown</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Overloads Function GetWebelement(ByVal definition As OpenQA.Selenium.By, ByVal timeoutSeconds As Integer) As IWebElement
        Return GetWebElement(driver, definition, timeoutSeconds)
    End Function
    ''' <summary>
    ''' Waits until the given element is enabled and visible
    ''' </summary>
    ''' <param name="webDriver"></param>
    ''' <param name="definition"></param>
    ''' <param name="seconds"></param>
    ''' <returns></returns>
    ''' <remarks>Needs to wait for .displayed because for e.g. in a collapsed Treeview all nodes are available but not visible 
    ''' if the parent node is collapsed and therefore the following error would appear:
    ''' OpenQA.Selenium.ElementNotVisibleException: Element is not currently visible and so may not be interacted with
    ''' </remarks>
    Private Overloads Function GetWebElement(ByVal webDriver As IWebDriver, ByVal definition As OpenQA.Selenium.By, ByVal seconds As Integer) As IWebElement
        Dim wait As New WebDriverWait(webDriver, TimeSpan.FromSeconds(seconds))
        wait.Until(Function(d)
                       Return d.FindElement(definition).Enabled And d.FindElement(definition).Displayed
                   End Function)
        GetWebElement = webDriver.FindElement(definition)
    End Function
End Class