using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleInspect_template.Err
{
    //public delegate void EventHandler_errCode(Int16 errCode);
    public delegate void EventHandler(ERR_RESULT result);

    public struct ERR_RESULT
    {
        public string funcName;
        public Int16  errCode;
        public Int16? Inner_errCode; //라이브러리나 다른 에러코드
        public string message;
        public string errTrace;
    }

    public class ErrProcess : Exception 
    {
        /*
         *  field
         */
        private Int16 m_ErrCode;

        /*
         *  property
         */
        public Int16 ErrCode
        {
            get { return m_ErrCode; }
        }
        
        /*
         *  delegate
         */
        public event EventHandler ActionCallback;
        
        /*
         *  constructor
         */

        /*
         *  method 
         */
        public ErrProcess()
        {
            ActionCallback = null;
            m_ErrCode = -1;
        }
        public ErrProcess(Int16 errCode)
        {
            ActionCallback = null;
            m_ErrCode = errCode;
        }
        
        static public ERR_RESULT SetErrResult(Exception err, Int16? InnerErr=null)
        {
            ERR_RESULT result = new ERR_RESULT();
            String[] ErrTrace = new String[255];

            ExtractErrTrace(err.StackTrace,ref ErrTrace);
            int pos = ErrTrace.Length;
            ErrProcess ep = err as ErrProcess; //에러코드와 그에 따른 메세지를 받기위해 
            if (ep == null) //예상외의 오류 
            {
                ep = new ErrProcess(-10); // 재정의
                result.funcName = ErrTrace[pos - 2]; // 호출된 함수 Name
                result.errTrace = ErrTrace[pos - 1]; // 에러 위치
                result.message = ep.GetErrMessage();
                result.Inner_errCode = InnerErr;
                result.errCode = ep.ErrCode;
            }

            else // 정의되어있는 오류 
            {
                result.funcName = ErrTrace[pos -2];      // 호출된 함수 Name
                result.errTrace = ErrTrace[pos -1];      // 에러 위치
                result.message = ep.GetErrMessage();
                result.Inner_errCode = InnerErr;
                result.errCode = ep.ErrCode;
            }
            return result;
        }
        static public ERR_RESULT SetErrResult_UserMessage(Exception err, Int16? InnerErr = null, String msg=null)
        {
            ERR_RESULT result = new ERR_RESULT();
            String[] ErrTrace = new String[255];

            ExtractErrTrace(err.StackTrace, ref ErrTrace);
            int pos = ErrTrace.Length;
            ErrProcess ep = err as ErrProcess; //에러코드와 그에 따른 메세지를 받기위해 
            if (ep == null) //예상외의 오류 
            {
                ep = new ErrProcess(-10); // 재정의
                result.funcName = ErrTrace[pos - 2]; // 호출된 함수 Name
                result.errTrace = ErrTrace[pos - 1]; // 에러 위치
                result.message = msg;
                result.Inner_errCode = InnerErr;
                result.errCode = ep.ErrCode;
            }

            else // 정의되어있는 오류 
            {
                result.funcName = ErrTrace[pos - 2];      // 호출된 함수 Name
                result.errTrace = ErrTrace[pos - 1];      // 에러 위치
                result.message = msg;
                result.Inner_errCode = InnerErr;
                result.errCode = ep.ErrCode;
            }
            return result;
        }
        private static void ExtractErrTrace(String errTrace,ref String[] extractData)
        {
            String[] token = new String[1] {"\r\n"};
            extractData = (errTrace).Split(token, StringSplitOptions.RemoveEmptyEntries);
            int pos = extractData.Length;
            String str = extractData[pos - 1];
            token = new String[2] { " 위치: ", "파일 "};
            extractData = (str).Split(token, StringSplitOptions.RemoveEmptyEntries);
        }
        /*
        public void SetErrCall(Int16 errCode)
        {
            m_ErrCode = ErrCode;

            ERR_RESULT result = new ERR_RESULT();
            result.errCode = errCode;
            result.message = GetErrMessage();

            if (ActionCallback == null)
                return;

            ActionCallback(result);
        }*/
        public void SetErrCall(ERR_RESULT err)
        {
            if (ActionCallback == null)
                return;

            ActionCallback(err);
        }

        public void ResetErr()
        {
            m_ErrCode = -1;
        }

        public String GetErrMessage()
        {
            String errMessage = null;

            switch (m_ErrCode)
            {
                case 0:
                     errMessage ="Succes";
                    break;
                case -1:
                    errMessage = "Not specified";
                    break;
                case -2:
                    errMessage = "It has not been modified or implemented in the future.";
                    break;
                case -3:
                    errMessage = "InspecterResultErr";
                    break;
                case -10:
                    errMessage = "System Err";
                    break;
                case -11:
                    errMessage = "Connection Err";
                    break;
                case -12:
                    errMessage = "IP format Err";
                    break;
                case -100:
                    errMessage = "Empty xmlfile";
                    break;
                case -101:
                    errMessage = "Incorrect xmlfile";
                    break;
                case -102:
                    errMessage = "No element value in the xmlfile";
                    break;
                case -103:
                    errMessage = "Contains elements that are not defined node";
                    break;
                case -200:
                    errMessage = "Not Select TargetModule";
                    break;
                case -300:
                    errMessage = "Not Insert Channel Size";
                    break;
                case -301:
                    errMessage = "Module Channel Size 4 or more";
                    break;
                case -302:
                    errMessage = "Target Serial Num length Err";
                    break;
                case -303:
                    errMessage = "Xml Serial Num length Err";
                    break;
                case -330:
                    errMessage = "Channel Size Over Flow";
                    break;
                case -331:
                    errMessage = "Cali Step Size Over Flow";
                    break;
                case -334:
                    errMessage = "The MacId packet format fo the Rx Buffer is incorrect.";
                    break;
                case -335:
                    errMessage = "Switch Setvalue Size Over Flow";
                    break;
                case -336:
                    errMessage = "MacId format si not match";
                    break;
                case -337:
                    errMessage = "NA System Consumption Curr Err";
                    break;
                case -338:
                    errMessage = "NA Field Consumption Curr Err";
                    break;
                case -340:
                    errMessage = "System current consumption Check Err";
                    break;
                case -341:
                    errMessage = "Field current consumption Check Err";
                    break;
                case -342:
                    errMessage = "Channel Check Err";
                    break;
                case -343:
                    errMessage = "Extend Check Err";
                    break;
                case -344:
                    errMessage = "AI TC module CJC Check Err";
                    break;
                case -345:
                    errMessage = "TC Channel per Channel ErrRate Err";
                    break;
                case -400:
                    errMessage = "Not powSupply232Open Err";
                    break;
                case -401:
                    errMessage = "ReadTimeOut Err";
                    break;
                case -402:
                    errMessage = "IsConnectPowerSupply Err";
                    break;
                case -403:
                    errMessage = "Can not Change ChannelScope Err";
                    break;
                case -404:
                    errMessage = "CurrValue Not Same SetVoltageVal Err";
                    break;
                case -405:
                    errMessage = "CurrValue Not Same SetCurrentVal Err";
                    break;
                case -406:
                    errMessage = "VoltageOutPutON Err";
                    break;
                case -407:
                    errMessage = "It is already Pow Open Err";
                    break;
                case -408:
                    errMessage = " Set value Err";
                    break;
                case -450:
                    errMessage = "FnIOLib Err";
                    break;
                case -451:
                    errMessage = "FnIOLib OpenDevice Err";
                    break;
                case -500:
                    errMessage = "Ip format is not correct";
                    break;
                case -501:
                    errMessage = "Already FnIO Device is open";
                    break;
                case -502:
                    errMessage = "Not found module property type and can't work linker";
                    break;
                case -503:
                    errMessage = "Not found module category type and can't work linker";
                    break;
                case -504:
                    errMessage = "Not found module channel";
                    break;
                case -505:
                    errMessage = "Already FnIODevice is Init";
                    break;
                case -600:
                    errMessage = "Pannel meta is not open";
                    break;
                case -607:
                    errMessage = "It is already Pannel Meta Open Err";
                    break;
                case -608:
                    errMessage = " Set value Err";
                    break;
                case -609:
                    errMessage = " crc Check Err";
                    break;
                case -700:
                    errMessage = "Not Found LogFile";
                    break;
                case -701:
                    errMessage = "Inspectlog items are Not Found";
                    break;
                case -800:
                    errMessage = "Cali Open Err";
                    break;
                case -998:
                    errMessage = "Target Module Parameter Size Err";
                    break;
                case -999:
                default:
                    //Unknown Err
                    errMessage = "Unknown Err";
                    break;
            }

            return errMessage;
        }
    }

    public class _XmlException : ErrProcess
    {
        public _XmlException()
        {}

        public _XmlException(Int16 errCode) : base(errCode)
        {}
    }
    public class _DIOInspectException : ErrProcess
    {
        public _DIOInspectException()
        {}

        public _DIOInspectException(Int16 errCode)
            : base(errCode)
        {}
    }
    public class _FnIOLibException : ErrProcess
    {
        public _FnIOLibException()
        {}

        public _FnIOLibException(Int16 errCode)
            : base(errCode)
        {}
    }
    public class _InspectEngin : ErrProcess
    {
        public _InspectEngin()
        {}

        public _InspectEngin(Int16 errCode)
            : base(errCode)
        {}
    }
    public class _MetaLibException : ErrProcess
    {
        public _MetaLibException()
        { }

        public _MetaLibException(Int16 errCode)
            : base(errCode)
        { }
    }
    public class _InspectLogFileException : ErrProcess
    {
        public _InspectLogFileException()
        { }

        public _InspectLogFileException(Int16 errCode)
            : base(errCode)
        { }
    }
    public class _MainException : ErrProcess
    {
        public _MainException()
        { }

        public _MainException(Int16 errCode)
            : base(errCode)
        { }
    }
    public class _CaliLibException : ErrProcess
    {
        public _CaliLibException()
        { }

        public _CaliLibException(Int16 errCode)
            : base(errCode)
        { }
    }
}
