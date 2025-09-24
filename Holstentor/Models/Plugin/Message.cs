using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Holstentor.Models.Plugin
{
    public class Message
    {
        public const string RequiredMsgDer = "Bitte geben Sie einen {0} ein.";
        public const string RequiredMsgDas = "Bitte geben Sie einen {0} ein.";
        public const string RequiredMsgDie = "Bitte geben Sie eine {0} ein.";
        public const string MaxLengthMsgDer = "Der {0} sollte nicht länger als {1} Zeichen sein";
        public const string MaxLengthMsgDas = "Das {0} sollte nicht länger als {1} Zeichen sein";
        public const string MaxLengthMsgDie = "Die {0} sollte nicht länger als {1} Zeichen sein";
        public const string MinLengthMsgDer = "Der {0} sollte nicht weniger als {1} Zeichen umfassen";
        public const string MinLengthMsgDas = "Das {0} sollte nicht weniger als {1} Zeichen umfassen";
        public const string MinLengthMsgDie = "Die {0} sollte nicht weniger als {1} Zeichen umfassen";
        // RegisterViewModel
        public const string RequiredMsgEmail = "Die {0} wird gebraucht.";
        public const string RequiredMsgPassword = "Das {0} wird gebraucht.";
        public const string Email = "Bitte geben Sie die richtige {0} ein.";
    }
}