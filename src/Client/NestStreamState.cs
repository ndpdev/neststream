using System;
using System.Text;

namespace NestStream.Client
{
    internal class NestStreamState
    {
        private enum MessagePart
        {
            EventType,
            DataFirstLine,
            DataContinue
        }

        private const string _prefixEvent = "event: ";
        private const string _prefixData = "data: ";

        private NestStreamClient _parent;
        private StringBuilder _currentMessageData;
        private string _currentMessageType;
        private MessagePart _currentPart;

        public NestStreamState(NestStreamClient parent)
        {
            _parent = parent;
            _currentMessageData = new StringBuilder();
            _currentMessageType = String.Empty;
            _currentPart = MessagePart.EventType;
        }

        public void ProcessLine(string inputLine)
        {
            switch (_currentPart)
            {
                case MessagePart.EventType: Handle_EventType(inputLine); break;
                case MessagePart.DataFirstLine: Handle_DataFirstLine(inputLine); break;
                case MessagePart.DataContinue: Handle_DataContinue(inputLine); break;
            }
        }

        private void ResetNestStreamState()
        {
            if (_currentMessageData.Length > 0) { _currentMessageData.Clear(); }
            _currentMessageType = String.Empty;
            _currentPart = MessagePart.EventType;
        }

        private void Handle_EventType(string inputLine)
        {
            if (inputLine.StartsWith(_prefixEvent))
            {
                _currentMessageType = inputLine.Substring(_prefixEvent.Length);
                _currentPart = MessagePart.DataFirstLine;
            }
        }

        private void Handle_DataFirstLine(string inputLine)
        {
            if (inputLine.StartsWith(_prefixData))
            {
                _currentMessageData.AppendLine(inputLine.Substring(_prefixData.Length));
                _currentPart = MessagePart.DataContinue;
            }
            else
            {
                ResetNestStreamState();
            }
        }

        private void Handle_DataContinue(string inputLine)
        {
            if (inputLine.Length == 0)
            {
                var args = new ReceivedNestStreamEventArgs(
                    DateTime.Now, _currentMessageType, _currentMessageData.ToString());

                _parent.RaiseNestStreamEvent(args);
                ResetNestStreamState();
            }
            else
            {
                _currentMessageData.AppendLine(inputLine);
            }
        }
    }
}