﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Servirtium.Core
{
    public interface IInteractionMonitor
    {
        void FinishedScript(int interactionNum, bool failed) { }

        Task<ServiceResponse> GetServiceResponseForRequest(Uri host,
                                                     IInteraction interaction,
                                                     bool lowerCaseHeaders);

        IInteraction NewInteraction(int interactionNum, String context, String method, String path, String url);

        void CodeNoteForNextInteraction(String title, String multiline) { }

        void NoteForNextInteraction(String title, String multiline) { }

    }
}
