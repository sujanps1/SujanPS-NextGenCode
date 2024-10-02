
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using HandlerReq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace HandlerReq
{
    public sealed class ApplicationConfiguration
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        private static extern bool WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        public string SEC_GENERAL = "General";
        public string SEC_HOST = "Host";
        public string SEC_STORE_AND_FORWARD = "StoreAndForward";
        public string SEC_PERIPHERALS = "Peripherals";
        public string SEC_PROXY = "Proxy";
        public string SEC_POSTING_SOURCE = "PostingSource";
        public string SEC_TRAINING_SOURCE = "TrainingModeSource";
        public string SEC_CAGE_XCHANGE = "CageXchange";

        public string SLOW_INTERNET = "SLOW_INTERNET";
        public string DOWNLOAD_FROM_XVIEW = "DOWNLOAD_FROM_XVIEW";
        public string RECORDS_PER_PAGE = "RECORDS_PER_PAGE";
        public string LOGIN_ATTEMPTS = "LOGIN_ATTEMPTS";
        public string CULTURE_SECTION = "CULTURE_SECTION";
        public string LOG_TO_FILE = "LOG_TO_FILE";
        public string LANGUAGE = "LANGUAGE";
        public string PRODUCT_NAME = "PRODUCT_NAME";
        public string RUN_MODE = "RUN_MODE";
        public string PINPAD_LOAD_SCREEN_TIMEOUT = "PINPAD_LOAD_SCREEN_TIMEOUT";
        public string SECURITY_PROTOCOL = "SECURITY_PROTOCOL";
        public string DEFAULT_COUNTRY = "DEFAULT_COUNTRY";
        public string DEFAULT_CURRENCY = "DEFAULT_CURRENCY";

        public string DELETE_OLD_LOGS_DAYS = "DELETE_OLD_LOGS_DAYS";
        public string DYR_WIDTH = "DYR_WIDTH";
        public string DYR_HEIGHT = "DYR_HEIGHT";
        public string ESEEK_UTILITY = "ESEEK_UTILITY_V2";
        public string ALLOW_SELF_SIGN = "ALLOW_SELF_SIGN_V2";

        public string SSL_ENABLED = "SSL_ENABLED";
        public string HOST_CALL_MODE = "HOST_CALL_MODE";
        public string CAGE_HOST_URL = "CAGE_HOST_URL";
        public string CAPS_SERVICE_URL = "CAPS_SERVICE_URL";
        public string CAGE_TERMINAL_ID = "CAGE_TERMINAL_ID";
        public string CAGE_SECURITY_CODE = "CAGE_SECURITY_CODE";
        public string PINPAD_HOST = "PINPAD_HOST";
        public string PINPAD_PORT = "PINPAD_PORT";
        public string PINPAD_TERMINAL_ID = "PINPAD_TERMINAL_ID";

        public string STORE_AND_FORWARD_ENABLED = "STORE_AND_FORWARD_ENABLED";
        public string ENCRYPT_FILE = "ENCRYPT_FILE";
        public string FILE_NAME_SEPARATOR = "FILE_NAME_SEPARATOR";
        public string INTERVAL = "INTERVAL";
        public string RETRY_ATTEMPTS = "RETRY_ATTEMPTS";

        public string ESEEK_SERIAL_PORT = "ESEEK_SERIAL_PORT";
        public string BARCODESCANNER_SERIAL_PORT = "BARCODESCANNER_SERIAL_PORT";
        public string INGENICO_SERIAL_PORT = "INGENICO_SERIAL_PORT";
        public string IS_HID = "IS_HID";
        public string POS_PRINTER_NAME = "POS_PRINTER_NAME";
        public string ENDORSMENT_PRINTER = "ENDORSMENT_PRINTER";
        public string PRINTER_PAPER_TYPE = "PRINTER_PAPER_TYPE";
        public string PAPER_SIZE = "PAPER_SIZE";
        public string MICR = "MICR";

        public string CAGE_HOST_PROXY_ADDRESS = "CAGE_HOST_PROXY_ADDRESS";
        public string CAGE_HOST_PROXY_PORT = "CAGE_HOST_PROXY_PORT";
        public string CAGE_HOST_USERNAME = "CAGE_HOST_USERNAME";
        public string CAGE_HOST_PASSWORD = "CAGE_HOST_PASSWORD";

        public string CAPS_PROXY_ADDRESS = "CAPS_PROXY_ADDRESS";
        public string CAPS_PROXY_PORT = "CAPS_PROXY_PORT";
        public string CAPS_PROXY_USERNAME = "CAPS_PROXY_USERNAME";
        public string CAPS_PROXY_PASSWORD = "CAPS_PROXY_PASSWORD";

        public string PINPAD_PROXY_ADDRESS = "PINPAD_PROXY_ADDRESS";
        public string PINPAD_PROXY_PORT = "PINPAD_PROXY_PORT";
        public string PINPAD_PROXY_USERNAME = "PINPAD_PROXY_USERNAME";
        public string PINPAD_PROXY_PASSWORD = "PINPAD_PROXY_PASSWORD";

        public string RA_SOURCE = "RA_SOURCE";
        public string NW_SOURCE = "NW_SOURCE";
        public string EVERIID_SOURCE = "EVERIID_SOURCE";
        public string UIM_SOURCE = "UIM_SOURCE";
        public string TRANSACTION_SOURCE = "TRANSACTION_SOURCE";
        public string FOXWOOD_SOURCE = "CASHCLUB_FW";
        public string SYNK31 = "CCSYNK31";
        public string RETRIEVAL_FLOW = "ENHANCED_RETRIEVAL_FLOW_V2";

        public string VERIDOC_TIME_LIMIT = "VERIDOC_TIME_LIMIT";
        public string VERIDOC_RETRY_MAX = "VERIDOC_RETRY_MAX";
        public string VERIDOC_RETRY_TIME = "VERIDOC_RETRY_TIME";
        public string VERIDOC_WRKSTN_NAME = "VERIDOC_WRKSTN_NAME";
        public string VERIDOC_FLOOR_NUM = "VERIDOC_FLOOR_NUM";
        public string VERIDOC_MRCHNT_NAME = "VERIDOC_MRCHNT_NAME";

        public string TRAINING_MODE = "TRAINING_MODE";
        public string DEVICE_RESTART_REQUIRED = "DEVICE_RESTART_REQUIRED";
        public string CLOSE_INTERVAL = "CLOSE_INTERVAL";

        public string DEMO_HOST_URL = "DEMO_HOST_URL";
        public string DEMO_CAPS_URL = "DEMO_CAPS_URL";
        public string DEMO_PINPAD_HOST = "DEMO_PINPAD_HOST";
        public string DEMO_PINPAD_PORT = "DEMO_PINPAD_PORT";

        public string PROD_HOST_URL = "PROD_HOST_URL";
        public string PROD_CAPS_URL = "PROD_CAPS_URL";
        public string PROD_PINPAD_HOST = "PROD_PINPAD_HOST";
        public string PROD_PINPAD_PORT = "PROD_PINPAD_PORT";

        private static string UPLOAD_LOCAL_SETTINGS = "UPLOAD_LOCAL_SETTINGS";
        private static string SKIP_XVIEW_SETTINGS = "SKIP_XVIEW_SETTINGS";

        public const string MALOG = "ANALYTIC_LOG";
        private string _iniPath;

        public static string XCHANGE_APPLICATION_PATH = "XCHANGE_APPLICATION_PATH";
        public static string XCHANGE_APPLICATION_TERMINAL_KEY = "XCHANGE_TERMINAL_KEY";
        public static string XCHANGE_CALL_BACK_URL = "XCHANGE_CALL_BACK_URL";
        public static string XCHANGE_TIMEOUT = "XCHANGE_TIMEOUT";
        public static string LOG_UPLOAD_INTERVAL = "LOG_UPLOAD_INTERVAL";

        public static string ENABLE_AMLPOST_ON_LOGIN = "ENABLE_AMLPOST_ON_LOGIN";

        public static string LOCAL_FEE_CALCULATION = "LOCAL_FEE_CALCULATION";
        public string CAPS_MODE_V1 = "CAPS_MODE_V1";
        public static string HEALTHCHECK_TIMEOUT = "HEALTHCHECK_TIMEOUT";
        public string PII_ENABLED = "PII_ENABLED";
        private Dictionary<string, string> _hostConfig;

        #region Public Properties
        public string DeviceRestartRequired { get { return DEVICE_RESTART_REQUIRED; } }
        public string TrainingSourceSection { get { return SEC_TRAINING_SOURCE; } }
        #endregion

        #region Properties

        #region General

        private bool _MALog;

        /// <summary>
        /// To Log analytic data to Local DB
        /// </summary>
        public bool AnalyticLog
        {
            get { return _MALog; }
            set { _MALog = value; }
        }

        private bool _SlowInternet;

        /// <summary>
        /// To display FormLoad in CashAdvacne screen during transaction updation
        /// </summary>
        public bool SlowInternet
        {
            get { return _SlowInternet; }
            set { _SlowInternet = value; }
        }

        int _iRecsPerPage;

        bool _isUploadLocalSettings;
        public bool IsUploadLocalSettingsOnBoot
        {
            get { return _isUploadLocalSettings; }
            set { _isUploadLocalSettings = value; }
        }
        bool isSkipLoadXViewSettings;

        public bool IsSkipLoadXViewSettings
        {
            get
            {
                return isSkipLoadXViewSettings;
            }
        }

        /// <summary>
        /// Records to be shown per page
        /// </summary>
        public int RecsPerPage
        {
            get { return _iRecsPerPage; }
            set { _iRecsPerPage = value; }
        }

        int _loginAtttempts;

        int _daysCountToDeleteOldLogs;

        public int LoginAtttempts
        {
            get { return _loginAtttempts; }
            set { _loginAtttempts = value; }
        }

        public int DaysCountToDeleteOldLogs
        {
            get { return _daysCountToDeleteOldLogs; }
            set { _daysCountToDeleteOldLogs = value; }
        }

        string _ReceiptHeight;
        /// <summary>
        /// Receipt Height for DYR from Ini file
        /// </summary>
        public string ReceiptHeight
        {
            get { return _ReceiptHeight; }
            set { _ReceiptHeight = value; }
        }

        string _ReceiptWidth;

        /// <summary>
        /// Receipt Width for DYR from Ini file
        /// </summary>
        public string ReceiptWidth
        {
            get { return _ReceiptWidth; }
            set { _ReceiptWidth = value; }
        }
        string _cultureSelection;
        public string CultureSelection
        {
            get
            {
                return _cultureSelection;
            }
            set
            {
                _cultureSelection = value;
            }
        }

        private bool _LogErrorToFile;

        public bool LogErrorToFile
        {
            get { return _LogErrorToFile; }
            set { _LogErrorToFile = value; }
        }
        string _productName;
        /// <summary>
        /// Product name
        /// </summary>
        public string ProductName
        {
            get
            {
                return _productName;
            }
            set
            {
                _productName = value;
            }
        }
        string _runMode;
        /// <summary>
        /// Run mode
        /// </summary>
        public string RunMode
        {
            get
            {
                return _runMode;
            }
            set
            {
                _runMode = value;
            }
        }

        private int _pinpadLoadScreenTimeout;

        /// <summary>
        /// Timer to display the load screen
        /// </summary>
        public int PinpadLoadScreenTimeout
        {
            get { return _pinpadLoadScreenTimeout; }
            set { _pinpadLoadScreenTimeout = value; }
        }

        string _defaultCountry;
        /// <summary>
        /// Default country
        /// </summary>
        public string DefaultCountry
        {
            get
            {
                return _defaultCountry;
            }
            set
            {
                _defaultCountry = value;
            }
        }

        string _defaultCurrency;
        /// <summary>
        /// Default currency
        /// </summary>
        public string DefaultCurrency
        {
            get
            {
                return _defaultCurrency;
            }
            set
            {
                _defaultCurrency = value;
            }
        }

        string _securityProtcol;

        public System.Net.SecurityProtocolType SecurityProtocolVersion
        {
            get
            {
                string[] protocolList = _securityProtcol?.Split(',');

                System.Net.SecurityProtocolType protocolTypes = System.Net.SecurityProtocolType.Tls12;
                System.Net.SecurityProtocolType protocolType;

                foreach (string strProtocol in protocolList)
                {
                    if (Enum.TryParse(strProtocol, out protocolType)) protocolTypes |= protocolType;
                }

                return protocolTypes;
            }
            set
            {
                System.Net.SecurityProtocolType[] protocolList = new System.Net.SecurityProtocolType[] { System.Net.SecurityProtocolType.Ssl3,
                                                                                                          System.Net.SecurityProtocolType.Tls,
                                                                                                          System.Net.SecurityProtocolType.Tls11,
                                                                                                          System.Net.SecurityProtocolType.Tls12
                                                                                                          };
                StringBuilder strProtocolList = new StringBuilder();

                foreach (System.Net.SecurityProtocolType protcol in protocolList)
                {
                    switch (value & protcol)
                    {
                        case System.Net.SecurityProtocolType.Ssl3: strProtocolList.Append((int)System.Net.SecurityProtocolType.Ssl3 + ","); break;

                        case System.Net.SecurityProtocolType.Tls: strProtocolList.Append((int)System.Net.SecurityProtocolType.Tls + ","); break;

                        case System.Net.SecurityProtocolType.Tls11: strProtocolList.Append((int)System.Net.SecurityProtocolType.Tls11 + ","); break;

                        case System.Net.SecurityProtocolType.Tls12: strProtocolList.Append((int)System.Net.SecurityProtocolType.Tls12 + ","); break;
                    }
                }

                _securityProtcol = strProtocolList.ToString();
            }
        }

        public string VersionInfo
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string VersionBuildNumber
        {
            get
            {
                return System.Diagnostics.FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion;
            }
        }

        private string _language;

        CultureInfo _CiLanguage;

        public CultureInfo Language
        {
            get
            {
                if (_CiLanguage == default(CultureInfo))
                {
                    _CiLanguage = new CultureInfo(_language);
                }
                return _CiLanguage;
            }
            set
            {
                if (value != default(CultureInfo))
                {
                    _language = value.Name;
                    _CiLanguage = value;
                }
            }
        }
        string _XchangeApllicationPath;
        /// <summary>
        /// Application Path for cage xchange application
        /// </summary>
        public string XchangeApllicationPath
        {
            get
            {
                return _XchangeApllicationPath;
            }
            set
            {
                _XchangeApllicationPath = value;
            }
        }
        string _CageXchangeCallBackURL;
        public string CageXchangeCallBackURL
        {
            get
            {
                return _CageXchangeCallBackURL;
            }
            set
            {
                _CageXchangeCallBackURL = value;
            }
        }

        private int _closeInterval;
        public int CloseInterval
        {
            get
            {
                return _closeInterval;
            }
            set
            {
                _closeInterval = value;
            }
        }

        int _CageXchangeTimeout;
        public int CageXchangeTimeout
        {
            get
            {
                return _CageXchangeTimeout;
            }
            set
            {
                _CageXchangeTimeout = value;
            }
        }

        int _LogUploadInterval;
        public int LogUploadInterval
        {
            get
            {
                return _LogUploadInterval;
            }
            set
            {
                _LogUploadInterval = value;
            }
        }

        int _HealthCheckTimeout;
        public int HealthCheckTimeout
        {
            get
            {
                return _HealthCheckTimeout;
            }
            set
            {
                _HealthCheckTimeout = value;
            }
        }
        #endregion

        #region Host

        private bool _IsSSLEnabled;

        public bool IsSSLEnabled
        {
            get { return _IsSSLEnabled; }
            set { _IsSSLEnabled = value; }
        }

        string _APICallMode;
        public string APICallMode
        {
            get
            {
                return _APICallMode;
            }
            set
            {
                _APICallMode = value;
            }
        }

        string cageHostURL;
        /// <summary>
        /// Cage Host Url
        /// </summary>
        public string CageHostURL
        {
            get
            {
                return cageHostURL;
            }
            set
            {
                cageHostURL = value;
            }
        }
        string _CAPSServiceURL;
        /// <summary>
        /// CAPS webservice Url
        /// </summary>
        public string CAPSServiceURL
        {
            get
            {
                return _CAPSServiceURL;
            }
            set
            {
                _CAPSServiceURL = value;
            }
        }

        /// <summary>
        /// Cash Club Cage Terminal ID
        /// </summary>
        public string CageTerminalID { get; set; }

        /// <summary>
        /// Security Code
        /// </summary>
        public string SecurityCode { get; set; }

        string _PinpadHostAddress;
        /// <summary>
        /// Pin pad host Address
        /// </summary>
        public string PinpadHostAddress
        {
            get
            {
                return _PinpadHostAddress;
            }
            set
            {
                _PinpadHostAddress = value;
            }
        }

        int _pinpadHostPort;

        /// <summary>
        /// Pinpad host port
        /// </summary>
        public int PinpadHostPort
        {
            get { return _pinpadHostPort; }
            set { _pinpadHostPort = value; }
        }

        /// <summary>
        /// Pinpad terminal id
        /// </summary>
        public string PinpadTerminalID { get; set; }

        #endregion

        #region Store and Foreward

        bool _forwardEnabled;

        public bool ForwardEnabled
        {
            get { return _forwardEnabled; }
            set { _forwardEnabled = value; }
        }

        bool _encryptFile;

        public bool EncryptFile
        {
            get { return _encryptFile; }
            set { _encryptFile = value; }
        }
        string _FileNameSeperator;
        public string FileNameSeperator
        {
            get
            {
                return _FileNameSeperator;
            }
            set
            {
                _FileNameSeperator = value;
            }
        }

        int _storeAndForwardInterval;

        public int StoreAndForwardInterval
        {
            get { return _storeAndForwardInterval; }
            set { _storeAndForwardInterval = value; }
        }

        int _forwardRetryAttempts;

        public int ForwardRetryAttempts
        {
            get { return _forwardRetryAttempts; }
            set { _forwardRetryAttempts = value; }
        }

        private bool _DisplayEseekUtility;
        public bool DisplayEseekUtility
        {
            get { return _DisplayEseekUtility; }
            set { _DisplayEseekUtility = value; }
        }

        private bool _AllowDummySSLCert;
        /// <summary>
        /// Added a new key for testing purpose, setting TRUE will skip the SSL certficate check for Konami Request
        /// </summary>
        public bool AllowDummySSLCert
        {
            get { return _AllowDummySSLCert; }
            set { _AllowDummySSLCert = value; }
        }
        #endregion

        #region Peripherals
        string _ESeekPort;
        /// <summary>
        /// Card reader port
        /// </summary>
        public string ESeekPort
        {
            get
            {
                return _ESeekPort;
            }
            set
            {
                _ESeekPort = value;
            }
        }
        string _BarcodeScannerPort;
        /// <summary>
        /// ID Scanner port
        /// </summary>
        public string BarcodeScannerPort
        {
            get
            {
                return _BarcodeScannerPort;
            }
            set
            {
                _BarcodeScannerPort = value;
            }
        }
        string _IngenicoPort;
        /// <summary>
        /// Ingenico port
        /// </summary>
        public string IngenicoPort
        {
            get
            {
                return _IngenicoPort;
            }
            set
            {
                _IngenicoPort = value;
            }
        }

        bool _IsHID;

        public bool IsHID
        {
            get { return _IsHID; }
            set { _IsHID = value; }
        }
        string _POSPrinterName;
        /// <summary>
        /// POS Printer Name
        /// </summary>
        public string POSPrinterName
        {
            get
            {
                return _POSPrinterName;
            }
            set
            {
                _POSPrinterName = value;
            }
        }

        string _POSEndorsementPrinter;
        /// <summary>
        /// POS Printer Name
        /// </summary>
        public string POSEndorsementPrinter
        {
            get
            {
                return _POSEndorsementPrinter;
            }
            set
            {
                _POSEndorsementPrinter = value;
            }
        }

        string _PrinterPaperType;
        /// <summary>
        /// Paper type
        /// </summary>
        public string PrinterPaperType
        {
            get
            {
                return _PrinterPaperType;
            }
            set
            {
                _PrinterPaperType = value;
            }
        }

        string _PaperSize;
        /// <summary>
        /// Paper size
        /// </summary>
        public string PaperSize
        {
            get
            {
                return _PaperSize;
            }
            set
            {
                _PaperSize = value;
            }
        }
        string _CheckReader;
        /// <summary>
        /// Check Reader Name
        /// </summary>
        public string CheckReader
        {
            get
            {
                return _CheckReader;
            }
            set
            {
                _CheckReader = value;
            }
        }

        private bool _enhancedRetrievalFlow;
        public bool EnhancedRetrievalFlow
        {
            get
            {
                return _enhancedRetrievalFlow;
            }
            set
            {
                _enhancedRetrievalFlow = value;
            }
        }

        private int _veridocsTimeLimit;
        public int VeridocsTimeLimit
        {
            get
            {
                return _veridocsTimeLimit;
            }
            set
            {
                _veridocsTimeLimit = value;
            }
        }

        private int _veridocsRetryMax;
        public int VeridocsRetryMax
        {
            get
            {
                return _veridocsRetryMax;
            }
            set
            {
                _veridocsRetryMax = value;
            }
        }

        private int _veridocsRetryTime;
        public int VeridocsRetryTime
        {
            get
            {
                return _veridocsRetryTime;
            }
            set
            {
                _veridocsRetryTime = value;
            }
        }

        private string _veridocsWorkstationName;
        public string VeridocsWorkstationName
        {
            get
            {
                return _veridocsWorkstationName;
            }
            set
            {
                _veridocsWorkstationName = value;
            }
        }

        private string _veridocsMerchantName;
        public string VeridocsMerchantName
        {
            get
            {
                return _veridocsMerchantName;
            }
            set
            {
                _veridocsMerchantName = value;
            }
        }

        private string _veridocsFloorNumber;
        public string VeridocsFloorNumber
        {
            get
            {
                return _veridocsFloorNumber;
            }
            set
            {
                _veridocsFloorNumber = value;
            }
        }
        public bool IsINIFileExists { get; private set; }

        #endregion

        #region Proxy
        string _HostProxyAddress;
        /// <summary>
        /// Proxy cage host address
        /// </summary>
        public string HostProxyAddress
        {
            get
            {
                return _HostProxyAddress;
            }
            set
            {
                _HostProxyAddress = value;
            }
        }

        int _hostProxyPort;

        /// <summary>
        /// Proxy cage port
        /// </summary>
        public int HostProxyPort
        {
            get { return _hostProxyPort; }
            set { _hostProxyPort = value; }
        }
        string _HostProxyUsername;
        /// <summary>
        /// Proxy cage host Username
        /// </summary>
        public string HostProxyUsername
        {
            get
            {
                return _HostProxyUsername;
            }
            set
            {
                _HostProxyUsername = value;
            }
        }
        string _HostProxyPassword;
        /// <summary>
        /// Proxy cage host password
        /// </summary>
        public string HostProxyPassword
        {
            get
            {
                return _HostProxyPassword;
            }
            set
            {
                _HostProxyPassword = value;
            }
        }

        string _CAPSProxyAddress;
        /// <summary>
        /// Proxy CAPS address
        /// </summary>
        public string CAPSProxyAddress
        {
            get
            {
                return _CAPSProxyAddress;
            }
            set
            {
                _CAPSProxyAddress = value;
            }
        }

        int _CAPSProxyPort;

        /// <summary>
        /// Proxy CAPS port
        /// </summary>
        public int CAPSProxyPort
        {
            get { return _CAPSProxyPort; }
            set { _CAPSProxyPort = value; }
        }
        string _CAPSProxyUsername;
        /// <summary>
        /// Proxy CAPS Username
        /// </summary>
        public string CAPSProxyUsername
        {
            get
            {
                return _CAPSProxyUsername;
            }
            set
            {
                _CAPSProxyUsername = value;
            }
        }
        string _CAPSProxyPassword;
        /// <summary>
        /// Proxy CAPS password
        /// </summary>
        public string CAPSProxyPassword
        {
            get
            {
                return _CAPSProxyPassword;
            }
            set
            {
                _CAPSProxyPassword = value;
            }
        }

        string _PinpadProxyAddress;
        /// <summary>
        /// Proxy pinpad cage API
        /// </summary>
        public string PinpadProxyAddress
        {
            get
            {
                return _PinpadProxyAddress;
            }
            set
            {
                _PinpadProxyAddress = value;
            }
        }

        int _pinpadProxyPort;

        /// <summary>
        /// Proxy pinpad port
        /// </summary>
        public int PinpadProxyPort
        {
            get { return _pinpadProxyPort; }
            set { _pinpadProxyPort = value; }
        }
        string _PinpadProxyUsername;
        /// <summary>
        /// Pinpad proxy username
        /// </summary>
        public string PinpadProxyUsername
        {
            get
            {
                return _PinpadProxyUsername;
            }
            set
            {
                _PinpadProxyUsername = value;
            }
        }
        string _PinpadProxyPassword;
        /// <summary>
        /// Pinpad proxy password
        /// </summary>
        public string PinpadProxyPassword
        {
            get
            {
                return _PinpadProxyPassword;
            }
            set
            {
                _PinpadProxyPassword = value;
            }
        }

        #endregion

        #region Posting Source
        string _RASource;
        public string RASource
        {
            get
            {
                return _RASource;
            }
            set
            {
                _RASource = value;
            }
        }
        string _NewaveSource;
        public string NewaveSource
        {
            get
            {
                return _NewaveSource;
            }
            set
            {
                _NewaveSource = value;
            }
        }
        string _EveriIDSource;
        public string EveriIDSource
        {
            get
            {
                return _EveriIDSource;
            }
            set
            {
                _EveriIDSource = value;
            }
        }
        string _UIMSource;
        public string UIMSource
        {
            get
            {
                return _UIMSource;
            }
            set
            {
                _UIMSource = value;
            }
        }
        string _TransactionLogCategory;
        public string TransactionLogCategory
        {
            get
            {
                return _TransactionLogCategory;
            }
            set
            {
                _TransactionLogCategory = value;
            }
        }
        string _FoxWoodSource;
        public string FoxWoodSource
        {
            get
            {
                return _FoxWoodSource;
            }
            set
            {
                _FoxWoodSource = value;
            }
        }
        string _Synk31Source;
        public string Synk31Source
        {
            get
            {
                return _Synk31Source;
            }
            set
            {
                _Synk31Source = value;
            }
        }

        #endregion

        private bool _capsMode;
        /// <summary>
        /// Caps mode 
        /// </summary>
        public bool CAPSMode
        {
            get
            {
                return _capsMode;
            }
            set
            {
                _capsMode = value;
            }
        }

        bool _EnableAMLOnLogin;
        public bool EnableAMLOnLogin
        {
            get
            {
                return _EnableAMLOnLogin;
            }
            set
            {
                _EnableAMLOnLogin = value;
            }
        }

        bool _localFeeCalculation;
        public bool LocalFeeCalculation
        {
            get { return _localFeeCalculation; }
            set { _localFeeCalculation = value; }
        }

        private bool _piiEnabled;
        /// <summary>
        /// Caps mode 
        /// </summary>
        public bool PIIEnabled
        {
            get
            {
                return _piiEnabled;
            }
            set
            {
                _piiEnabled = value;
            }
        }
        #endregion

        #region Class Variables

        /// <summary>
        /// Instance for application configuration
        /// </summary>
        private static ApplicationConfiguration _instance = default(ApplicationConfiguration);

        /// <summary>
        /// Lock object
        /// </summary>
        private static readonly object _objLock = new object();

        #endregion

        #region Constructor

        /// <summary>
        /// Private constructor
        /// </summary>
        ApplicationConfiguration()
        {
            _iniPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            this.IsINIFileExists = true;
            //Create directory if not found
            if (!Directory.Exists(_iniPath))
            {
                Directory.CreateDirectory(_iniPath);
            }

            //Terminal INI file Path
            _iniPath = Path.Combine(_iniPath, "EviVanilla.ini");

            // Write to the file
            if (File.Exists(_iniPath))
            {
                File.SetAttributes(_iniPath, FileAttributes.Normal);
            }

            _hostConfig = new Dictionary<string, string>();

            /*            ReadApplicationConfiguration();
            */
        }

        #endregion

        #region Public Methods

        public static ApplicationConfiguration Instance
        {
            get
            {
                lock (_objLock)
                {
                    if (_instance == null)
                    {
                        _instance = new ApplicationConfiguration();
                    }

                    return _instance;
                }
            }
        }

        #endregion

        /*    public void ResetUploadFlag()
            {
                WriteIniFile(SEC_GENERAL, UPLOAD_LOCAL_SETTINGS, false);
            }

            #region ReadIniFile

            // Reads the Initialization File 
            public string ReadIniFile(string section, string key, string defaultValue = "")
            {
                string iniData;
                try
                {
                    if (!File.Exists(_iniPath))
                    {
                        this.IsINIFileExists = false;
                        return defaultValue;
                    }

                    StringBuilder temp = new StringBuilder(500);
                    int i = GetPrivateProfileString(section, key, "", temp, 500, _iniPath);
                    iniData = temp.ToString();
                    if (string.IsNullOrWhiteSpace(iniData))
                        return defaultValue;
                    else
                        return iniData.Trim();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            #endregion

            #region WriteIniFile

            // Write the Initialization File 
            public bool WriteIniFile(string section, string key, object data)
            {
                try
                {
                    if (this.IsINIFileExists == false)
                        this.IsINIFileExists = true;
                    return WritePrivateProfileString(section, key, Convert.ToString(data), _iniPath);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            #endregion

            #region Read and write Configurations

            public void DisableEnhancedRetrievalFlow()
            {
                WriteIniFile(SEC_GENERAL, RETRIEVAL_FLOW, false);
            }
            public void WriteApplicationConfiguration()
            {
                WriteIniFile(SEC_GENERAL, SLOW_INTERNET, SlowInternet);
                WriteIniFile(SEC_GENERAL, RECORDS_PER_PAGE, RecsPerPage);
                WriteIniFile(SEC_GENERAL, LOGIN_ATTEMPTS, LoginAtttempts);
                WriteIniFile(SEC_GENERAL, CULTURE_SECTION, CultureSelection);
                WriteIniFile(SEC_GENERAL, LOG_TO_FILE, LogErrorToFile);
                WriteIniFile(SEC_GENERAL, LANGUAGE, _language);
                WriteIniFile(SEC_GENERAL, PRODUCT_NAME, ProductName);
                WriteIniFile(SEC_GENERAL, RUN_MODE, RunMode);
                WriteIniFile(SEC_GENERAL, PINPAD_LOAD_SCREEN_TIMEOUT, PinpadLoadScreenTimeout);
                WriteIniFile(SEC_GENERAL, DEFAULT_COUNTRY, DefaultCountry);
                WriteIniFile(SEC_GENERAL, DEFAULT_CURRENCY, DefaultCurrency);
                WriteIniFile(SEC_GENERAL, SECURITY_PROTOCOL, _securityProtcol);
                //WriteIniFile(SEC_GENERAL, UPLOAD_LOCAL_SETTINGS, _isUploadLocalSettings);

                WriteIniFile(SEC_GENERAL, DELETE_OLD_LOGS_DAYS, DaysCountToDeleteOldLogs);
                WriteIniFile(SEC_GENERAL, DYR_HEIGHT, ReceiptHeight);
                WriteIniFile(SEC_GENERAL, DYR_WIDTH, ReceiptWidth);
                WriteIniFile(SEC_GENERAL, ESEEK_UTILITY, DisplayEseekUtility);
                WriteIniFile(SEC_GENERAL, ALLOW_SELF_SIGN, AllowDummySSLCert);
                WriteIniFile(SEC_GENERAL, MALOG, AnalyticLog);
                WriteIniFile(SEC_GENERAL, CLOSE_INTERVAL, CloseInterval);
                WriteIniFile(SEC_GENERAL, ENABLE_AMLPOST_ON_LOGIN, EnableAMLOnLogin);

                WriteIniFile(SEC_GENERAL, VERIDOC_TIME_LIMIT, VeridocsTimeLimit);
                WriteIniFile(SEC_GENERAL, VERIDOC_RETRY_MAX, VeridocsRetryMax);
                WriteIniFile(SEC_GENERAL, VERIDOC_RETRY_TIME, VeridocsRetryTime);
                WriteIniFile(SEC_GENERAL, VERIDOC_WRKSTN_NAME, VeridocsWorkstationName);
                WriteIniFile(SEC_GENERAL, VERIDOC_MRCHNT_NAME, VeridocsMerchantName);
                WriteIniFile(SEC_GENERAL, VERIDOC_FLOOR_NUM, VeridocsFloorNumber);

                WriteIniFile(SEC_HOST, SSL_ENABLED, IsSSLEnabled);
                WriteIniFile(SEC_HOST, HOST_CALL_MODE, APICallMode);
                WriteIniFile(SEC_HOST, CAGE_HOST_URL, CageHostURL);
                WriteIniFile(SEC_HOST, CAPS_SERVICE_URL, CAPSServiceURL);
                WriteIniFile(SEC_HOST, CAGE_TERMINAL_ID, CageTerminalID);
                WriteIniFile(SEC_HOST, CAGE_SECURITY_CODE, SecurityCode);
                WriteIniFile(SEC_HOST, PINPAD_HOST, PinpadHostAddress);
                WriteIniFile(SEC_HOST, PINPAD_PORT, PinpadHostPort);
                WriteIniFile(SEC_HOST, PINPAD_TERMINAL_ID, PinpadTerminalID);
                WriteIniFile(SEC_HOST, HEALTHCHECK_TIMEOUT, HealthCheckTimeout);

                WriteIniFile(SEC_STORE_AND_FORWARD, STORE_AND_FORWARD_ENABLED, ForwardEnabled);
                WriteIniFile(SEC_STORE_AND_FORWARD, ENCRYPT_FILE, EncryptFile);
                WriteIniFile(SEC_STORE_AND_FORWARD, FILE_NAME_SEPARATOR, FileNameSeperator);
                WriteIniFile(SEC_STORE_AND_FORWARD, INTERVAL, StoreAndForwardInterval);
                WriteIniFile(SEC_STORE_AND_FORWARD, RETRY_ATTEMPTS, ForwardRetryAttempts);

                WriteIniFile(SEC_PERIPHERALS, ESEEK_SERIAL_PORT, ESeekPort);
                WriteIniFile(SEC_PERIPHERALS, BARCODESCANNER_SERIAL_PORT, BarcodeScannerPort);
                WriteIniFile(SEC_PERIPHERALS, INGENICO_SERIAL_PORT, IngenicoPort);
                WriteIniFile(SEC_PERIPHERALS, IS_HID, IsHID);
                WriteIniFile(SEC_PERIPHERALS, POS_PRINTER_NAME, POSPrinterName);
                WriteIniFile(SEC_PERIPHERALS, ENDORSMENT_PRINTER, POSEndorsementPrinter);
                WriteIniFile(SEC_PERIPHERALS, PRINTER_PAPER_TYPE, PrinterPaperType);
                WriteIniFile(SEC_PERIPHERALS, PAPER_SIZE, PaperSize);
                WriteIniFile(SEC_PERIPHERALS, MICR, CheckReader);

                WriteIniFile(SEC_PROXY, CAGE_HOST_PROXY_ADDRESS, HostProxyAddress);
                WriteIniFile(SEC_PROXY, CAGE_HOST_PROXY_PORT, HostProxyPort);
                WriteIniFile(SEC_PROXY, CAGE_HOST_USERNAME, HostProxyUsername);
                WriteIniFile(SEC_PROXY, CAGE_HOST_PASSWORD, HostProxyPassword);
                //WriteIniFile(SEC_PROXY, CAPS_PROXY_ADDRESS, CAPSProxyAddress);
                //WriteIniFile(SEC_PROXY, CAPS_PROXY_PORT, CAPSProxyPort);
                //WriteIniFile(SEC_PROXY, CAPS_PROXY_USERNAME, CAPSProxyUsername);
                //WriteIniFile(SEC_PROXY, CAPS_PROXY_PASSWORD, CAPSProxyPassword);
                //WriteIniFile(SEC_PROXY, PINPAD_PROXY_ADDRESS, PinpadProxyAddress);
                //WriteIniFile(SEC_PROXY, PINPAD_PROXY_PORT, PinpadProxyPort);
                //WriteIniFile(SEC_PROXY, PINPAD_PROXY_USERNAME, PinpadProxyUsername);
                //WriteIniFile(SEC_PROXY, PINPAD_PROXY_PASSWORD, PinpadProxyPassword);

                WriteIniFile(SEC_POSTING_SOURCE, RA_SOURCE, RASource);
                WriteIniFile(SEC_POSTING_SOURCE, NW_SOURCE, NewaveSource);
                WriteIniFile(SEC_POSTING_SOURCE, EVERIID_SOURCE, EveriIDSource);
                WriteIniFile(SEC_POSTING_SOURCE, UIM_SOURCE, UIMSource);
                WriteIniFile(SEC_POSTING_SOURCE, TRANSACTION_SOURCE, TransactionLogCategory);
                WriteIniFile(SEC_POSTING_SOURCE, FOXWOOD_SOURCE, FoxWoodSource);
                WriteIniFile(SEC_POSTING_SOURCE, SYNK31, Synk31Source);

                WriteIniFile(SEC_TRAINING_SOURCE, TRAINING_MODE, IsTrainingMode);
                WriteIniFile(SEC_TRAINING_SOURCE, DEVICE_RESTART_REQUIRED, IsDeviceRestartRequired);

                //WriteIniFile(SEC_TRAINING_SOURCE, DEMO_HOST_URL, DemoHostURL);
                //WriteIniFile(SEC_TRAINING_SOURCE, DEMO_CAPS_URL, DemoCAPSURL);
                //WriteIniFile(SEC_TRAINING_SOURCE, DEMO_PINPAD_HOST, DemoPinpadPort);
                //WriteIniFile(SEC_TRAINING_SOURCE, DEMO_PINPAD_PORT, DemoPinpadPort);

                //WriteIniFile(SEC_TRAINING_SOURCE, PROD_HOST_URL, ProdHostURL);
                //WriteIniFile(SEC_TRAINING_SOURCE, PROD_CAPS_URL, ProdCAPSURL);
                //WriteIniFile(SEC_TRAINING_SOURCE, PROD_PINPAD_HOST, ProdPinpadHost);
                //WriteIniFile(SEC_TRAINING_SOURCE, PROD_PINPAD_PORT, ProdPinpadPort);

                WriteIniFile(SEC_GENERAL, XCHANGE_APPLICATION_PATH, XchangeApllicationPath);
                //WriteIniFile(SEC_CAGE_XCHANGE, XCHANGE_APPLICATION_TERMINAL_KEY, CageXchangeLiteTerminalKey);
                WriteIniFile(SEC_CAGE_XCHANGE, XCHANGE_CALL_BACK_URL, CageXchangeCallBackURL);
                WriteIniFile(SEC_GENERAL, CAPS_MODE_V1, CAPSMode);
                WriteIniFile(SEC_CAGE_XCHANGE, XCHANGE_TIMEOUT, CageXchangeTimeout);
                WriteIniFile(SEC_GENERAL, LOG_UPLOAD_INTERVAL, LogUploadInterval);

                WriteIniFile(SEC_GENERAL, LOCAL_FEE_CALCULATION, LocalFeeCalculation);
                WriteIniFile(SEC_GENERAL, PII_ENABLED, PIIEnabled);
            }

            public void ReadApplicationConfiguration()
            {
                bool.TryParse(ReadIniFile(SEC_GENERAL, SLOW_INTERNET, "true"), out _SlowInternet);
                int.TryParse(ReadIniFile(SEC_GENERAL, RECORDS_PER_PAGE, "20"), out _iRecsPerPage);
                int.TryParse(ReadIniFile(SEC_GENERAL, LOGIN_ATTEMPTS, "3"), out _loginAtttempts);
                CultureSelection = ReadIniFile(SEC_GENERAL, CULTURE_SECTION, "Merchant");
                bool.TryParse(ReadIniFile(SEC_GENERAL, LOG_TO_FILE, "true"), out _LogErrorToFile);
                _language = ReadIniFile(SEC_GENERAL, LANGUAGE, "en-us");
                ProductName = ReadIniFile(SEC_GENERAL, PRODUCT_NAME, "CashClub");
                RunMode = ReadIniFile(SEC_GENERAL, RUN_MODE, "NORMAL");
                int.TryParse(ReadIniFile(SEC_GENERAL, PINPAD_LOAD_SCREEN_TIMEOUT, "65"), out _pinpadLoadScreenTimeout);
                bool.TryParse(ReadIniFile(SEC_GENERAL, RETRIEVAL_FLOW, "true"), out _enhancedRetrievalFlow);
                DefaultCountry = ReadIniFile(SEC_GENERAL, DEFAULT_COUNTRY, "USA");
                DefaultCurrency = ReadIniFile(SEC_GENERAL, DEFAULT_CURRENCY, "USD");
                _securityProtcol = ReadIniFile(SEC_GENERAL, SECURITY_PROTOCOL, "48,192,768,3072");

                int.TryParse(ReadIniFile(SEC_GENERAL, DELETE_OLD_LOGS_DAYS, "14"), out _daysCountToDeleteOldLogs);
                _ReceiptHeight = ReadIniFile(SEC_GENERAL, DYR_HEIGHT, "22");
                _ReceiptWidth = ReadIniFile(SEC_GENERAL, DYR_WIDTH, "2.84");
                bool.TryParse(ReadIniFile(SEC_GENERAL, MALOG, "true"), out _MALog);
                CloseInterval = int.Parse(ReadIniFile(SEC_GENERAL, CLOSE_INTERVAL, "4"));
                bool.TryParse(ReadIniFile(SEC_GENERAL, ENABLE_AMLPOST_ON_LOGIN, "true"), out _EnableAMLOnLogin);

                bool.TryParse(ReadIniFile(SEC_HOST, SSL_ENABLED, "true"), out _IsSSLEnabled);
                APICallMode = ReadIniFile(SEC_HOST, HOST_CALL_MODE, "WEB");
                CageHostURL = ReadIniFile(SEC_HOST, CAGE_HOST_URL, "https://cashclub.everi.com/CashClubVanillaHost/CageWebService.svc");
                CAPSServiceURL = ReadIniFile(SEC_HOST, CAPS_SERVICE_URL, "https://caps.everisolutions.com/cash-access");
                CageTerminalID = ReadIniFile(SEC_HOST, CAGE_TERMINAL_ID, "");
                SecurityCode = ReadIniFile(SEC_HOST, CAGE_SECURITY_CODE, "");
                PinpadHostAddress = ReadIniFile(SEC_HOST, PINPAD_HOST, "posconnect.everi.com");
                int.TryParse(ReadIniFile(SEC_HOST, PINPAD_PORT, "50012"), out _pinpadHostPort);
                PinpadTerminalID = ReadIniFile(SEC_HOST, PINPAD_TERMINAL_ID, "");
                int.TryParse(ReadIniFile(SEC_HOST, HEALTHCHECK_TIMEOUT, "15"), out _HealthCheckTimeout);

                bool.TryParse(ReadIniFile(SEC_STORE_AND_FORWARD, STORE_AND_FORWARD_ENABLED, "true"), out _forwardEnabled);
                bool.TryParse(ReadIniFile(SEC_STORE_AND_FORWARD, ENCRYPT_FILE, "true"), out _encryptFile);
                FileNameSeperator = ReadIniFile(SEC_STORE_AND_FORWARD, FILE_NAME_SEPARATOR, "_");
                int.TryParse(ReadIniFile(SEC_STORE_AND_FORWARD, INTERVAL, "30"), out _storeAndForwardInterval);
                int.TryParse(ReadIniFile(SEC_STORE_AND_FORWARD, RETRY_ATTEMPTS, "3"), out _forwardRetryAttempts);

                ESeekPort = ReadIniFile(SEC_PERIPHERALS, ESEEK_SERIAL_PORT, "");
                BarcodeScannerPort = ReadIniFile(SEC_PERIPHERALS, BARCODESCANNER_SERIAL_PORT, "");
                IngenicoPort = ReadIniFile(SEC_PERIPHERALS, INGENICO_SERIAL_PORT, "");
                bool.TryParse(ReadIniFile(SEC_PERIPHERALS, IS_HID, "false"), out _IsHID);
                POSPrinterName = ReadIniFile(SEC_PERIPHERALS, POS_PRINTER_NAME, "");
                POSEndorsementPrinter = ReadIniFile(SEC_PERIPHERALS, ENDORSMENT_PRINTER, "");
                PrinterPaperType = ReadIniFile(SEC_PERIPHERALS, PRINTER_PAPER_TYPE, "");
                PaperSize = ReadIniFile(SEC_PERIPHERALS, PAPER_SIZE, "");
                CheckReader = ReadIniFile(SEC_PERIPHERALS, MICR, "");

                CAPSProxyAddress = PinpadProxyAddress = HostProxyAddress = ReadIniFile(SEC_PROXY, CAGE_HOST_PROXY_ADDRESS, "");
                int.TryParse(ReadIniFile(SEC_PROXY, CAGE_HOST_PROXY_PORT, "0"), out _hostProxyPort);
                _CAPSProxyPort = _pinpadProxyPort = _hostProxyPort;
                CAPSProxyUsername = PinpadProxyUsername = HostProxyUsername = ReadIniFile(SEC_PROXY, CAGE_HOST_USERNAME, "");
                CAPSProxyPassword = PinpadProxyPassword = HostProxyPassword = ReadIniFile(SEC_PROXY, CAGE_HOST_PASSWORD, "");

                //PinpadProxyAddress = ReadIniFile(SEC_PROXY, PINPAD_PROXY_ADDRESS, "");
                //int.TryParse(ReadIniFile(SEC_PROXY, PINPAD_PROXY_PORT, "0"), out _pinpadProxyPort);
                //PinpadProxyUsername = ReadIniFile(SEC_PROXY, PINPAD_PROXY_USERNAME, "");
                //PinpadProxyPassword = ReadIniFile(SEC_PROXY, PINPAD_PROXY_PASSWORD, "");

                //CAPSProxyAddress = ReadIniFile(SEC_PROXY, CAPS_PROXY_ADDRESS, "");
                //int.TryParse(ReadIniFile(SEC_PROXY, CAPS_PROXY_PORT, "0"), out _CAPSProxyPort);
                //CAPSProxyUsername = ReadIniFile(SEC_PROXY, CAPS_PROXY_USERNAME, "");
                //CAPSProxyPassword = ReadIniFile(SEC_PROXY, CAPS_PROXY_PASSWORD, "");

                RASource = ReadIniFile(SEC_PROXY, RA_SOURCE, "CASHCLUB_RA");
                NewaveSource = ReadIniFile(SEC_PROXY, NW_SOURCE, "CASHCLUB_NW");
                EveriIDSource = ReadIniFile(SEC_PROXY, EVERIID_SOURCE, "EveriID_OFAC");
                UIMSource = ReadIniFile(SEC_PROXY, UIM_SOURCE, "UIM");
                TransactionLogCategory = ReadIniFile(SEC_PROXY, TRANSACTION_SOURCE, "TITANIUM_SYSTEM");
                FoxWoodSource = ReadIniFile(SEC_PROXY, FOXWOOD_SOURCE, "CASHCLUB_FW");
                Synk31Source = ReadIniFile(SEC_PROXY, SYNK31, "CCSYNK31");

                bool.TryParse(ReadIniFile(SEC_GENERAL, ESEEK_UTILITY, "true"), out _DisplayEseekUtility);
                bool.TryParse(ReadIniFile(SEC_GENERAL, ALLOW_SELF_SIGN, "false"), out _AllowDummySSLCert);

                bool.TryParse(ReadIniFile(SEC_TRAINING_SOURCE, TRAINING_MODE, "false"), out _IsTrainingMode);
                bool.TryParse(ReadIniFile(SEC_TRAINING_SOURCE, DEVICE_RESTART_REQUIRED, "false"), out _isDeviceRestartRequired);

                bool.TryParse(ReadIniFile(SEC_GENERAL, UPLOAD_LOCAL_SETTINGS, "false"), out _isUploadLocalSettings);
                bool.TryParse(ReadIniFile(SEC_GENERAL, SKIP_XVIEW_SETTINGS, "false"), out isSkipLoadXViewSettings);

                XchangeApllicationPath = ReadIniFile(SEC_GENERAL, XCHANGE_APPLICATION_PATH, "");
                CageXchangeCallBackURL = ReadIniFile(SEC_CAGE_XCHANGE, XCHANGE_CALL_BACK_URL, "http://localhost:8001/");
                CAPSMode = bool.Parse(ReadIniFile(SEC_GENERAL, CAPS_MODE_V1, "true"));

                int.TryParse(ReadIniFile(SEC_CAGE_XCHANGE, XCHANGE_TIMEOUT, "300"), out _CageXchangeTimeout);
                int.TryParse(ReadIniFile(SEC_GENERAL, LOG_UPLOAD_INTERVAL, "15"), out _LogUploadInterval);

                int.TryParse(ReadIniFile(SEC_GENERAL, VERIDOC_TIME_LIMIT, "300"), out _veridocsTimeLimit);
                int.TryParse(ReadIniFile(SEC_GENERAL, VERIDOC_RETRY_MAX, "3"), out _veridocsRetryMax);
                int.TryParse(ReadIniFile(SEC_GENERAL, VERIDOC_RETRY_TIME, "5"), out _veridocsRetryTime);
                VeridocsWorkstationName = ReadIniFile(SEC_GENERAL, VERIDOC_WRKSTN_NAME, "");
                VeridocsMerchantName = ReadIniFile(SEC_GENERAL, VERIDOC_MRCHNT_NAME, "");
                VeridocsFloorNumber = ReadIniFile(SEC_GENERAL, VERIDOC_FLOOR_NUM, "");
                bool.TryParse(ReadIniFile(SEC_GENERAL, LOCAL_FEE_CALCULATION, "true"), out _localFeeCalculation);
                PIIEnabled = bool.Parse(ReadIniFile(SEC_GENERAL, PII_ENABLED, "false"));
            }

            #endregion

            #region INIFromXView
            public void ConsumeXViewLocalSettings(string terminalSettings, string merchantSettings, bool isCheckForDownloadRequired, out bool isUploadRequired, out string auditLog)
            {
                auditLog = string.Empty;
                isUploadRequired = false;
                bool isWriteiniRequired = false;



                if (string.IsNullOrEmpty(merchantSettings) == false)
                {
                    List<KeyValuePair<string, string>> settingValues = JsonConvert.DeserializeObject<List<KeyValuePair<string, string>>>(merchantSettings);

                    CompareAndUpdateLatestConfig(settingValues, CAGE_HOST_URL, "https://cashclub.everi.com/CashClubVanillaHost/CageWebService.svc", true, ref cageHostURL, ref isUploadRequired, ref isWriteiniRequired, ref auditLog);
                    CompareAndUpdateLatestConfig(settingValues, CAPS_SERVICE_URL, "https://caps.everisolutions.com/cash-access", true, ref _CAPSServiceURL, ref isUploadRequired, ref isWriteiniRequired, ref auditLog);
                    CompareAndUpdateLatestConfig(settingValues, PINPAD_HOST, "posconnect.everi.com", true, ref _PinpadHostAddress, ref isUploadRequired, ref isWriteiniRequired, ref auditLog);
                    CompareAndUpdateLatestConfig(settingValues, PINPAD_PORT, 50012, true, ref _pinpadHostPort, ref isUploadRequired, ref isWriteiniRequired, ref auditLog);
                }

                if (string.IsNullOrEmpty(terminalSettings) == false)
                {
                   *//* List<KeyValuePair<string, string>> settingValues = JsonConvert.DeserializeObject<List<KeyValuePair<string, string>>>(terminalSettings);

                    string downloadedValue = string.Empty;
                    KeyValuePair<string, string> downloadRequired = settingValues.Where(x => x.Key == DOWNLOAD_FROM_XVIEW).FirstOrDefault();
                    bool isDownloadRequired = true;
                    if (isCheckForDownloadRequired && downloadRequired.Equals(default(KeyValuePair<string, string>)) == false)
                    {
                        if (Convert.ToBoolean(downloadRequired.Value) == false)
                        {
                            isDownloadRequired = false;
                        }
                        else
                        {
                            auditLog = "Uploading to reset Download_From_XView Configuration";
                            isUploadRequired = true;//Need to reset the DOWNLOAD_FROM_XVIEW value to False and resend the value back to server! 
                        }
                    }

                    CompareAndUpdateLatestConfig(settingValues, SLOW_INTERNET, true, isDownloadRequired, ref _SlowInternet, ref isUploadRequired, ref isWriteiniRequired, ref auditLog);
                    CompareAndUpdateLatestConfig(settingValues, RECORDS_PER_PAGE, 20, isDownloadRequired, ref _iRecsPerPage, ref isUploadRequired, ref isWriteiniRequired, ref auditLog);
                    CompareAndUpdateLatestConfig(settingValues, LOGIN_ATTEMPTS, 3, isDownloadRequired, ref _loginAtttempts, ref isUploadRequired, ref isWriteiniRequired, ref auditLog);
                    CompareAndUpdateLatestConfig(settingValues, CULTURE_SECTION, "Merchant", isDownloadRequired, ref _cultureSelection, ref isUploadRequired, ref isWriteiniRequired, ref auditLog);
                    CompareAndUpdateLatestConfig(settingValues, LOG_TO_FILE, true, isDownloadRequired, ref _LogErrorToFile, ref isUploadRequired, ref isWriteiniRequired, ref auditLog);
                    CompareAndUpdateLatestConfig(settingValues, LANGUAGE, "en-US", isDownloadRequired, ref _language, ref isUploadRequired, ref isWriteiniRequired, ref auditLog);
                    CompareAndUpdateLatestConfig(settingValues, PRODUCT_NAME, "CashClub", isDownloadRequired, ref _productName, ref isUploadRequired, ref isWriteiniRequired, ref auditLog);
                    CompareAndUpdateLatestConfig(settingValues, RUN_MODE, "NORMAL", isDownloadRequired, ref _runMode, ref isUploadRequired, ref isWriteiniRequired, ref auditLog);
                    CompareAndUpdateLatestConfig(settingValues, PINPAD_LOAD_SCREEN_TIMEOUT, 65, isDownloadRequired, ref _pinpadLoadScreenTimeout, ref isUploadRequired, ref isWriteiniRequired, ref auditLog);
                    CompareAndUpdateLatestConfig(settingValues, DEFAULT_COUNTRY, "USA", isDownloadRequired, ref _defaultCountry, ref isUploadRequired, ref isWriteiniRequired, ref auditLog);
                    CompareAndUpdateLatestConfig(settingValues, DEFAULT_CURRENCY, "USD", isDownloadRequired, ref _defaultCurrency, ref isUploadRequired, ref isWriteiniRequired, ref auditLog);
                    CompareAndUpdateLatestConfig(settingValues, SECURITY_PROTOCOL, "48,192,768,3072", isDownloadRequired, ref _securityProtcol, ref isUploadRequired, ref isWriteiniRequired, ref auditLog);
                    CompareAndUpdateLatestConfig(settingValues, DELETE_OLD_LOGS_DAYS, 14, isDownloadRequired, ref _daysCountToDeleteOldLogs, ref isUploadRequired, ref isWriteiniRequired, ref auditLog);
                    CompareAndUpdateLatestConfig(settingValues, DYR_HEIGHT, "22", isDownloadRequired, ref _ReceiptHeight, ref isUploadRequired, ref isWriteiniRequired, ref auditLog);
                    CompareAndUpdateLatestConfig(settingValues, DYR_WIDTH, "2.84", isDownloadRequired, ref _ReceiptWidth, ref isUploadRequired, ref isWriteiniRequired, ref auditLog);
                    CompareAndUpdateLatestConfig(settingValues, ESEEK_UTILITY, true, isDownloadRequired, ref _DisplayEseekUtility, ref isUploadRequired, ref isWriteiniRequired, ref auditLog);
                    CompareAndUpdateLatestConfig(settingValues, ALLOW_SELF_SIGN, false, isDownloadRequired, ref _AllowDummySSLCert, ref isUploadRequired, ref isWriteiniRequired, ref auditLog);
                    CompareAndUpdateLatestConfig(settingValues, MALOG, true, isDownloadRequired, ref _MALog, ref isUploadRequired, ref isWriteiniRequired, ref auditLog);
                    CompareAndUpdateLatestConfig(settingValues, CLOSE_INTERVAL, 4, isDownloadRequired, ref _closeInterval, ref isUploadRequired, ref isWriteiniRequired, ref auditLog);
                    CompareAndUpdateLatestConfig(settingValues, SSL_ENABLED, true, isDownloadRequired, ref _IsSSLEnabled, ref isUploadRequired, ref isWriteiniRequired, ref auditLog);
                    CompareAndUpdateLatestConfig(settingValues, HOST_CALL_MODE, "WEB", isDownloadRequired, ref _APICallMode, ref isUploadRequired, ref isWriteiniRequired, ref auditLog);
                    CompareAndUpdateLatestConfig(settingValues, STORE_AND_FORWARD_ENABLED, true, isDownloadRequired, ref _forwardEnabled, ref isUploadRequired, ref isWriteiniRequired, ref auditLog);
                    CompareAndUpdateLatestConfig(settingValues, ENCRYPT_FILE, true, isDownloadRequired, ref _encryptFile, ref isUploadRequired, ref isWriteiniRequired, ref auditLog);
                    CompareAndUpdateLatestConfig(settingValues, FILE_NAME_SEPARATOR, "_", isDownloadRequired, ref _FileNameSeperator, ref isUploadRequired, ref isWriteiniRequired, ref auditLog);
                    CompareAndUpdateLatestConfig(settingValues, INTERVAL, 30, isDownloadRequired, ref _storeAndForwardInterval, ref isUploadRequired, ref isWriteiniRequired, ref auditLog);
                    CompareAndUpdateLatestConfig(settingValues, RETRY_ATTEMPTS, 3, isDownloadRequired, ref _forwardRetryAttempts, ref isUploadRequired, ref isWriteiniRequired, ref auditLog);
                    CompareAndUpdateLatestConfig(settingValues, ESEEK_SERIAL_PORT, "", isDownloadRequired, ref _ESeekPort, ref isUploadRequired, ref isWriteiniRequired, ref auditLog, isAlwaysInCapital: true);
                    CompareAndUpdateLatestConfig(settingValues, BARCODESCANNER_SERIAL_PORT, "", isDownloadRequired, ref _BarcodeScannerPort, ref isUploadRequired, ref isWriteiniRequired, ref auditLog, isAlwaysInCapital: true);
                    CompareAndUpdateLatestConfig(settingValues, INGENICO_SERIAL_PORT, "", isDownloadRequired, ref _IngenicoPort, ref isUploadRequired, ref isWriteiniRequired, ref auditLog, isAlwaysInCapital: true);
                    CompareAndUpdateLatestConfig(settingValues, IS_HID, false, isDownloadRequired, ref _IsHID, ref isUploadRequired, ref isWriteiniRequired, ref auditLog);
                    CompareAndUpdateLatestConfig(settingValues, POS_PRINTER_NAME, "", isDownloadRequired, ref _POSPrinterName, ref isUploadRequired, ref isWriteiniRequired, ref auditLog);
                    CompareAndUpdateLatestConfig(settingValues, ENDORSMENT_PRINTER, "", isDownloadRequired, ref _POSEndorsementPrinter, ref isUploadRequired, ref isWriteiniRequired, ref auditLog);
                    CompareAndUpdateLatestConfig(settingValues, PRINTER_PAPER_TYPE, "", isDownloadRequired, ref _PrinterPaperType, ref isUploadRequired, ref isWriteiniRequired, ref auditLog);
                    CompareAndUpdateLatestConfig(settingValues, PAPER_SIZE, "", isDownloadRequired, ref _PaperSize, ref isUploadRequired, ref isWriteiniRequired, ref auditLog);
                    CompareAndUpdateLatestConfig(settingValues, MICR, "", isDownloadRequired, ref _CheckReader, ref isUploadRequired, ref isWriteiniRequired, ref auditLog);
                    CompareAndUpdateLatestConfig(settingValues, CAGE_HOST_PROXY_ADDRESS, "", isDownloadRequired, ref _HostProxyAddress, ref isUploadRequired, ref isWriteiniRequired, ref auditLog);
                    CompareAndUpdateLatestConfig(settingValues, CAGE_HOST_PROXY_PORT, 0, isDownloadRequired, ref _hostProxyPort, ref isUploadRequired, ref isWriteiniRequired, ref auditLog);
                    CompareAndUpdateLatestConfig(settingValues, CAGE_HOST_USERNAME, "", isDownloadRequired, ref _HostProxyUsername, ref isUploadRequired, ref isWriteiniRequired, ref auditLog);
                    CompareAndUpdateLatestConfig(settingValues, CAGE_HOST_PASSWORD, "", isDownloadRequired, ref _HostProxyPassword, ref isUploadRequired, ref isWriteiniRequired, ref auditLog);
                    //CompareAndUpdateLatestConfig(settingValues, CAPS_PROXY_ADDRESS, "", isDownloadRequired, ref _CAPSProxyAddress, ref isUploadRequired, ref isWriteiniRequired, ref auditLog);
                    //CompareAndUpdateLatestConfig(settingValues, CAPS_PROXY_PORT, 0, isDownloadRequired, ref _CAPSProxyPort, ref isUploadRequired, ref isWriteiniRequired, ref auditLog);
                    //CompareAndUpdateLatestConfig(settingValues, CAPS_PROXY_USERNAME, "", isDownloadRequired, ref _CAPSProxyUsername, ref isUploadRequired, ref isWriteiniRequired, ref auditLog);
                    //CompareAndUpdateLatestConfig(settingValues, CAPS_PROXY_PASSWORD, "", isDownloadRequired, ref _CAPSProxyPassword, ref isUploadRequired, ref isWriteiniRequired, ref auditLog);

                    //CompareAndUpdateLatestConfig(settingValues, PINPAD_PROXY_ADDRESS, "", isDownloadRequired, ref _PinpadProxyAddress, ref isUploadRequired, ref isWriteiniRequired, ref auditLog);
                    //CompareAndUpdateLatestConfig(settingValues, PINPAD_PROXY_PORT, 0, isDownloadRequired, ref _pinpadProxyPort, ref isUploadRequired, ref isWriteiniRequired, ref auditLog);
                    //CompareAndUpdateLatestConfig(settingValues, PINPAD_PROXY_USERNAME, "", isDownloadRequired, ref _PinpadProxyUsername, ref isUploadRequired, ref isWriteiniRequired, ref auditLog);
                    //CompareAndUpdateLatestConfig(settingValues, PINPAD_PROXY_PASSWORD, "", isDownloadRequired, ref _PinpadProxyPassword, ref isUploadRequired, ref isWriteiniRequired, ref auditLog);

                    #region [Updating all proxy variable]
                    CAPSProxyAddress = PinpadProxyAddress = HostProxyAddress;
                    _CAPSProxyPort = _pinpadProxyPort = _hostProxyPort;
                    CAPSProxyUsername = PinpadProxyUsername = HostProxyUsername;
                    CAPSProxyPassword = PinpadProxyPassword = HostProxyPassword;
                    #endregion

                    CompareAndUpdateLatestConfig(settingValues, RA_SOURCE, "", isDownloadRequired, ref _RASource, ref isUploadRequired, ref isWriteiniRequired, ref auditLog);
                    CompareAndUpdateLatestConfig(settingValues, NW_SOURCE, "", isDownloadRequired, ref _NewaveSource, ref isUploadRequired, ref isWriteiniRequired, ref auditLog);
                    CompareAndUpdateLatestConfig(settingValues, EVERIID_SOURCE, "", isDownloadRequired, ref _EveriIDSource, ref isUploadRequired, ref isWriteiniRequired, ref auditLog);
                    CompareAndUpdateLatestConfig(settingValues, UIM_SOURCE, "", isDownloadRequired, ref _UIMSource, ref isUploadRequired, ref isWriteiniRequired, ref auditLog);
                    CompareAndUpdateLatestConfig(settingValues, TRANSACTION_SOURCE, "TITANIUM_SYSTEM", isDownloadRequired, ref _TransactionLogCategory, ref isUploadRequired, ref isWriteiniRequired, ref auditLog);
                    CompareAndUpdateLatestConfig(settingValues, FOXWOOD_SOURCE, "CASHCLUB_FW", isDownloadRequired, ref _FoxWoodSource, ref isUploadRequired, ref isWriteiniRequired, ref auditLog);
                    CompareAndUpdateLatestConfig(settingValues, SYNK31, "CCSYNK31", isDownloadRequired, ref _Synk31Source, ref isUploadRequired, ref isWriteiniRequired, ref auditLog);
                    CompareAndUpdateLatestConfig(settingValues, TRAINING_MODE, false, isDownloadRequired, ref _IsTrainingMode, ref isUploadRequired, ref isWriteiniRequired, ref auditLog);
                    CompareAndUpdateLatestConfig(settingValues, XCHANGE_APPLICATION_PATH, "", isDownloadRequired, ref _XchangeApllicationPath, ref isUploadRequired, ref isWriteiniRequired, ref auditLog);
                    CompareAndUpdateLatestConfig(settingValues, XCHANGE_CALL_BACK_URL, "http://localhost:8001/", isDownloadRequired, ref _CageXchangeCallBackURL, ref isUploadRequired, ref isWriteiniRequired, ref auditLog);
                    CompareAndUpdateLatestConfig(settingValues, CAPS_MODE_V1, true, isDownloadRequired, ref _capsMode, ref isUploadRequired, ref isWriteiniRequired, ref auditLog);
                    CompareAndUpdateLatestConfig(settingValues, XCHANGE_TIMEOUT, 300, isDownloadRequired, ref _CageXchangeTimeout, ref isUploadRequired, ref isWriteiniRequired, ref auditLog);
                    CompareAndUpdateLatestConfig(settingValues, LOG_UPLOAD_INTERVAL, 15, isDownloadRequired, ref _LogUploadInterval, ref isUploadRequired, ref isWriteiniRequired, ref auditLog);

                    CompareAndUpdateLatestConfig(settingValues, VERIDOC_RETRY_MAX, 3, isDownloadRequired, ref _veridocsRetryMax, ref isUploadRequired, ref isWriteiniRequired, ref auditLog);
                    CompareAndUpdateLatestConfig(settingValues, VERIDOC_TIME_LIMIT, 300, isDownloadRequired, ref _veridocsTimeLimit, ref isUploadRequired, ref isWriteiniRequired, ref auditLog);
                    CompareAndUpdateLatestConfig(settingValues, VERIDOC_RETRY_TIME, 5, isDownloadRequired, ref _veridocsRetryTime, ref isUploadRequired, ref isWriteiniRequired, ref auditLog);
                    CompareAndUpdateLatestConfig(settingValues, VERIDOC_MRCHNT_NAME, "", isDownloadRequired, ref _veridocsMerchantName, ref isUploadRequired, ref isWriteiniRequired, ref auditLog);
                    CompareAndUpdateLatestConfig(settingValues, VERIDOC_FLOOR_NUM, "", isDownloadRequired, ref _veridocsFloorNumber, ref isUploadRequired, ref isWriteiniRequired, ref auditLog);
                    CompareAndUpdateLatestConfig(settingValues, VERIDOC_WRKSTN_NAME, "", isDownloadRequired, ref _veridocsWorkstationName, ref isUploadRequired, ref isWriteiniRequired, ref auditLog);
                    CompareAndUpdateLatestConfig(settingValues, ENABLE_AMLPOST_ON_LOGIN, true, isDownloadRequired, ref _EnableAMLOnLogin, ref isUploadRequired, ref isWriteiniRequired, ref auditLog);
                    CompareAndUpdateLatestConfig(settingValues, HEALTHCHECK_TIMEOUT, 15, isDownloadRequired, ref _HealthCheckTimeout, ref isUploadRequired, ref isWriteiniRequired, ref auditLog);
                    CompareAndUpdateLatestConfig(settingValues, PII_ENABLED, false, isDownloadRequired, ref _piiEnabled, ref isUploadRequired, ref isWriteiniRequired, ref auditLog);

                }
    *//*
                if (isWriteiniRequired)
                {
                    WriteApplicationConfiguration();
                }
            }

            private void CompareAndUpdateLatestConfig<T>(List<KeyValuePair<string, string>> settings, string key, T defaultValue, bool isDownloadRequired,
                ref T configValue, ref bool isUploadRequired, ref bool isWriteIniRequired, ref string auditLogs, bool isAlwaysInCapital = false)
            {
                KeyValuePair<string, string> setting = settings.Where(x => x.Key == key).FirstOrDefault();

                if (setting.Equals(default(KeyValuePair<string, string>)) == false)
                {
                    if (isDownloadRequired == false)
                    {
                        return;
                    }
                    //Previously we were comparing the configValue & setting with ToUpperInvariant. But we had to remove it as if there were 'case' changes, the local INI will not consume the change.
                    //Bug: 543016
                    if (configValue?.ToString().Trim() != setting.Value?.Trim())
                    {
                        try
                        {
                            if (key == "LANGUAGE")
                                configValue = (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(ValidateLanguage(setting.Value.ToLowerInvariant(), ref isUploadRequired, ref auditLogs, key));
                            else
                                configValue = (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(isAlwaysInCapital ? setting.Value.ToUpperInvariant() : setting.Value);
                            //isAlwaysInCaptial will be provided for the COM ports, as even if we configure as Com1 on XView, it should be saved in the local as COM1. Else App Config screen will not work. 
                            //Bug: 543016
                        }
                        catch
                        {
                            configValue = defaultValue;
                            isUploadRequired = true;
                            auditLogs += Environment.NewLine + string.Format("Resetting Key {0} to DefaultVal {1} due to parsing issue.", key, defaultValue.ToString());
                        }

                        isWriteIniRequired = true;
                    }
                }
                else
                {
                    auditLogs += Environment.NewLine + string.Format("Added New Key {0} with Value {1}.", key, configValue.ToString());

                    isUploadRequired = true;
                }
            }

            private string ValidateLanguage(string configValue, ref bool isUploadRequired, ref string auditLogs, string key)
            {
                string language = "en-us";
                string[] languages = { "en-us", "ar-ae", "en-gb", "es-es", "fr-fr", "zh-cn" };
                if (languages.Any(configValue.Contains))
                    language = configValue;
                else
                {
                    isUploadRequired = true;
                    auditLogs += Environment.NewLine + string.Format("Resetting Key {0} to DefaultVal {1} due to parsing issue.", key, language);
                }
                return language;
            }

            public void GetConfigurationToUpload()
            {
                *//*List<KeyValuePair<string, string>> configurations = new List<System.Collections.Generic.KeyValuePair<string, string>>();

                configurations.Add(new KeyValuePair<string, string>(SLOW_INTERNET, SlowInternet.ToString()));
                configurations.Add(new KeyValuePair<string, string>(RECORDS_PER_PAGE, RecsPerPage.ToString()));
                configurations.Add(new KeyValuePair<string, string>(LOGIN_ATTEMPTS, LoginAtttempts.ToString()));
                configurations.Add(new KeyValuePair<string, string>(CULTURE_SECTION, CultureSelection.ToString()));
                configurations.Add(new KeyValuePair<string, string>(LOG_TO_FILE, LogErrorToFile.ToString()));
                configurations.Add(new KeyValuePair<string, string>(LANGUAGE, _language.ToString()));
                configurations.Add(new KeyValuePair<string, string>(PRODUCT_NAME, ProductName.ToString()));
                configurations.Add(new KeyValuePair<string, string>(RUN_MODE, RunMode.ToString()));
                configurations.Add(new KeyValuePair<string, string>(PINPAD_LOAD_SCREEN_TIMEOUT, PinpadLoadScreenTimeout.ToString()));
                configurations.Add(new KeyValuePair<string, string>(DEFAULT_COUNTRY, DefaultCountry.ToString()));
                configurations.Add(new KeyValuePair<string, string>(DEFAULT_CURRENCY, DefaultCurrency.ToString()));
                configurations.Add(new KeyValuePair<string, string>(SECURITY_PROTOCOL, _securityProtcol.ToString()));
                configurations.Add(new KeyValuePair<string, string>(DELETE_OLD_LOGS_DAYS, DaysCountToDeleteOldLogs.ToString()));
                configurations.Add(new KeyValuePair<string, string>(DYR_HEIGHT, ReceiptHeight.ToString()));
                configurations.Add(new KeyValuePair<string, string>(DYR_WIDTH, ReceiptWidth.ToString()));
                configurations.Add(new KeyValuePair<string, string>(ESEEK_UTILITY, DisplayEseekUtility.ToString()));
                configurations.Add(new KeyValuePair<string, string>(ALLOW_SELF_SIGN, AllowDummySSLCert.ToString()));
                //configurations.Add(new KeyValuePair<string, string>(RETRIEVAL_FLOW, EnhancedRetrievalFlow.ToString()));
                configurations.Add(new KeyValuePair<string, string>(MALOG, AnalyticLog.ToString()));
                configurations.Add(new KeyValuePair<string, string>(CLOSE_INTERVAL, CloseInterval.ToString()));
                configurations.Add(new KeyValuePair<string, string>(SSL_ENABLED, IsSSLEnabled.ToString()));
                configurations.Add(new KeyValuePair<string, string>(HOST_CALL_MODE, APICallMode.ToString()));
                configurations.Add(new KeyValuePair<string, string>(STORE_AND_FORWARD_ENABLED, ForwardEnabled.ToString()));
                configurations.Add(new KeyValuePair<string, string>(ENCRYPT_FILE, EncryptFile.ToString()));
                configurations.Add(new KeyValuePair<string, string>(FILE_NAME_SEPARATOR, FileNameSeperator.ToString()));
                configurations.Add(new KeyValuePair<string, string>(INTERVAL, StoreAndForwardInterval.ToString()));
                configurations.Add(new KeyValuePair<string, string>(RETRY_ATTEMPTS, ForwardRetryAttempts.ToString()));
                configurations.Add(new KeyValuePair<string, string>(ESEEK_SERIAL_PORT, ESeekPort.ToString()));
                configurations.Add(new KeyValuePair<string, string>(BARCODESCANNER_SERIAL_PORT, BarcodeScannerPort.ToString()));
                configurations.Add(new KeyValuePair<string, string>(INGENICO_SERIAL_PORT, IngenicoPort.ToString()));
                configurations.Add(new KeyValuePair<string, string>(IS_HID, IsHID.ToString()));
                configurations.Add(new KeyValuePair<string, string>(POS_PRINTER_NAME, POSPrinterName.ToString()));
                configurations.Add(new KeyValuePair<string, string>(ENDORSMENT_PRINTER, POSEndorsementPrinter.ToString()));
                configurations.Add(new KeyValuePair<string, string>(PRINTER_PAPER_TYPE, PrinterPaperType.ToString()));
                configurations.Add(new KeyValuePair<string, string>(PAPER_SIZE, PaperSize.ToString()));
                configurations.Add(new KeyValuePair<string, string>(MICR, CheckReader.ToString()));
                configurations.Add(new KeyValuePair<string, string>(CAGE_HOST_PROXY_ADDRESS, HostProxyAddress.ToString()));
                configurations.Add(new KeyValuePair<string, string>(CAGE_HOST_PROXY_PORT, HostProxyPort.ToString()));
                configurations.Add(new KeyValuePair<string, string>(CAGE_HOST_USERNAME, HostProxyUsername.ToString()));
                configurations.Add(new KeyValuePair<string, string>(CAGE_HOST_PASSWORD, HostProxyPassword.ToString()));
                //configurations.Add(new KeyValuePair<string, string>(PINPAD_PROXY_ADDRESS, PinpadProxyAddress.ToString()));
                //configurations.Add(new KeyValuePair<string, string>(PINPAD_PROXY_PORT, PinpadProxyPort.ToString()));
                //configurations.Add(new KeyValuePair<string, string>(PINPAD_PROXY_USERNAME, PinpadProxyUsername.ToString()));
                //configurations.Add(new KeyValuePair<string, string>(PINPAD_PROXY_PASSWORD, PinpadProxyPassword.ToString()));
                //configurations.Add(new KeyValuePair<string, string>(CAPS_PROXY_ADDRESS, CAPSProxyAddress.ToString()));
                //configurations.Add(new KeyValuePair<string, string>(CAPS_PROXY_PORT, CAPSProxyPort.ToString()));
                //configurations.Add(new KeyValuePair<string, string>(CAPS_PROXY_USERNAME, CAPSProxyUsername.ToString()));
                //configurations.Add(new KeyValuePair<string, string>(CAPS_PROXY_PASSWORD, CAPSProxyPassword.ToString()));
                configurations.Add(new KeyValuePair<string, string>(RA_SOURCE, RASource.ToString()));
                configurations.Add(new KeyValuePair<string, string>(NW_SOURCE, NewaveSource.ToString()));
                configurations.Add(new KeyValuePair<string, string>(EVERIID_SOURCE, EveriIDSource.ToString()));
                configurations.Add(new KeyValuePair<string, string>(UIM_SOURCE, UIMSource.ToString()));
                configurations.Add(new KeyValuePair<string, string>(TRANSACTION_SOURCE, TransactionLogCategory.ToString()));
                configurations.Add(new KeyValuePair<string, string>(FOXWOOD_SOURCE, FoxWoodSource.ToString()));
                configurations.Add(new KeyValuePair<string, string>(SYNK31, Synk31Source.ToString()));
                configurations.Add(new KeyValuePair<string, string>(TRAINING_MODE, IsTrainingMode.ToString()));
                configurations.Add(new KeyValuePair<string, string>(XCHANGE_APPLICATION_PATH, XchangeApllicationPath?.ToString()));
                configurations.Add(new KeyValuePair<string, string>(XCHANGE_CALL_BACK_URL, CageXchangeCallBackURL?.ToString()));
                configurations.Add(new KeyValuePair<string, string>(DOWNLOAD_FROM_XVIEW, false.ToString()));
                configurations.Add(new KeyValuePair<string, string>(CAPS_MODE_V1, CAPSMode.ToString()));
                configurations.Add(new KeyValuePair<string, string>(XCHANGE_TIMEOUT, CageXchangeTimeout.ToString()));
                configurations.Add(new KeyValuePair<string, string>(LOG_UPLOAD_INTERVAL, LogUploadInterval.ToString()));

                configurations.Add(new KeyValuePair<string, string>(VERIDOC_RETRY_MAX, VeridocsRetryMax.ToString()));
                configurations.Add(new KeyValuePair<string, string>(VERIDOC_RETRY_TIME, VeridocsRetryTime.ToString()));
                configurations.Add(new KeyValuePair<string, string>(VERIDOC_TIME_LIMIT, VeridocsTimeLimit.ToString()));
                configurations.Add(new KeyValuePair<string, string>(VERIDOC_FLOOR_NUM, VeridocsFloorNumber));
                configurations.Add(new KeyValuePair<string, string>(VERIDOC_WRKSTN_NAME, VeridocsWorkstationName));
                configurations.Add(new KeyValuePair<string, string>(VERIDOC_MRCHNT_NAME, VeridocsMerchantName));
                configurations.Add(new KeyValuePair<string, string>(ENABLE_AMLPOST_ON_LOGIN, EnableAMLOnLogin.ToString()));
                configurations.Add(new KeyValuePair<string, string>(LOCAL_FEE_CALCULATION, LocalFeeCalculation.ToString()));
                configurations.Add(new KeyValuePair<string, string>(HEALTHCHECK_TIMEOUT, HealthCheckTimeout.ToString()));
                configurations.Add(new KeyValuePair<string, string>(PII_ENABLED, PIIEnabled.ToString()));

                return JsonConvert.SerializeObject(configurations);*//*
            }
            #endregion

            #region Get Host Config

            public void LoadHostConfigs(string configString)
            {
                if (string.IsNullOrWhiteSpace(configString))
                {
                    _hostConfig = null;
                }
                else
                {
    *//*                _hostConfig = JsonConvert.DeserializeObject<Dictionary<string, string>>(configString);
    *//*            }
            }

            public AnyType GetHostConfigValue<AnyType>(string key, AnyType defaultValue)
            {
                AnyType value = default(AnyType);

                if (_hostConfig != null && _hostConfig.ContainsKey(key))
                {
                    value = (AnyType)Convert.ChangeType(_hostConfig[key], typeof(AnyType));
                }

                return value;
            }

            public void LoadHostDefaultValues()
            {
                if (string.IsNullOrWhiteSpace(XchangeApllicationPath)) XchangeApllicationPath = GetHostConfigValue(XCHANGE_APPLICATION_PATH, @"C:\UIM\UIM.exe");

                WriteApplicationConfiguration();
            }
    */
        /*        #endregion
        */
        #region[TRAINING MODE]

        private bool _IsTrainingMode;
        public bool IsTrainingMode
        {
            get { return _IsTrainingMode; }
            set { _IsTrainingMode = value; }
        }

        private bool _isDeviceRestartRequired;

        public bool IsDeviceRestartRequired
        {
            get
            {
                return _isDeviceRestartRequired;
            }
            set
            {
                _isDeviceRestartRequired = value;
            }
        }

        private bool _IsAutoConfirm;
        public bool IsAutoConfirm
        {
            get { return _IsAutoConfirm; }
            set { _IsAutoConfirm = value; }
        }

        private int _AutoConfirmTimer;
        public int AutoConfirmTimer
        {
            get
            {
                return _AutoConfirmTimer;
            }
            set
            {
                _AutoConfirmTimer = value;
            }
        }
        //public string DemoHostURL { get; set; }
        //public string DemoCAPSURL { get; set; }
        //public string DemoPinpadHost { get; set; }
        //public string DemoPinpadPort { get; set; }
        //public string ProdHostURL { get; set; }
        //public string ProdCAPSURL { get; set; }
        //public string ProdPinpadHost { get; set; }
        //public string ProdPinpadPort { get; set; }

        #endregion[TRAINING MODE]
    }
}