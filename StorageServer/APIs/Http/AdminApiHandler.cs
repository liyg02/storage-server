﻿using System;
using System.Net;
using System.Threading;
using SyslogLogging;
using WatsonWebserver;

namespace Kvpbase
{
    public partial class StorageServer
    {
        static HttpResponse AdminApiHandler(RequestMetadata md)
        {
            #region Enumerate

            _Logging.Log(LoggingModule.Severity.Debug, 
                "AdminApiHandler admin API requested by " + 
                md.Http.SourceIp + ":" + md.Http.SourcePort + " " + 
                md.Http.Method + " " + md.Http.RawUrlWithoutQuery);

            #endregion

            #region Metadata

            if (md.Params.RequestMetadata)
            {
                RequestMetadata respMetadata = md.Sanitized();
                return new HttpResponse(md.Http, true, 200, null, "application/json", Common.SerializeJson(respMetadata, true), true);
            }

            #endregion

            #region Process-Request

            switch (md.Http.Method)
            {
                case HttpMethod.GET:
                    #region get

                    if (WatsonCommon.UrlEqual(md.Http.RawUrlWithoutQuery, "/admin/disks", false))
                    {
                        return HttpGetDisks(md);
                    }

                    if (WatsonCommon.UrlEqual(md.Http.RawUrlWithoutQuery, "/admin/topology", false))
                    {
                        return HttpGetTopology(md);
                    }

                    break;

                #endregion

                case HttpMethod.PUT:
                    #region put

                    break;

                #endregion

                case HttpMethod.POST:
                    #region post
                     
                    break;

                #endregion

                case HttpMethod.DELETE:
                    #region delete
                     
                    break;

                #endregion

                case HttpMethod.HEAD:
                    #region head

                    break;

                #endregion 
            }

            _Logging.Log(LoggingModule.Severity.Warn, "AdminApiHandler unknown endpoint URL: " + md.Http.RawUrlWithoutQuery);
            return new HttpResponse(md.Http, false, 400, null, "application/json", 
                new ErrorResponse(2, 400, "Unknown endpoint.", null), true);

            #endregion
        }
    }
}