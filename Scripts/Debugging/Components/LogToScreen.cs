﻿//----------------------------------------------
// Flip Web Apps: Game Framework
// Copyright © 2016 Flip Web Apps / Mark Hewitt
//
// Please direct any bugs/comments/suggestions to http://www.flipwebapps.com
// 
// The copyright owner grants to the end user a non-exclusive, worldwide, and perpetual license to this Asset
// to integrate only as incorporated and embedded components of electronic games and interactive media and 
// distribute such electronic game and interactive media. End user may modify Assets. End user may otherwise 
// not reproduce, distribute, sublicense, rent, lease or lend the Assets. It is emphasized that the end 
// user shall not be entitled to distribute or transfer in any way (including, without, limitation by way of 
// sublicense) the Assets in any other way than as integrated components of electronic games and interactive media. 

// The above copyright notice and this permission notice must not be removed from any files.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO
// THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
// TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//----------------------------------------------

using System.Collections;
using UnityEngine;

namespace FlipWebApps.GameFramework.Scripts.Debugging.Components
{
    /// <summary>
    /// Displays the current log onto the screen for debugging purposes
    /// </summary>
    [AddComponentMenu("Game Framework/Debugging/LogToScreen")]
    [HelpURL("http://www.flipwebapps.com/game-framework/")]
    public class LogToScreen : MonoBehaviour
    {
        static string _myLog;
        static readonly Queue LogMessagesQueue = new Queue();
        public string Output = "";
        public string Stack = "";
        bool _hidden = true;
        public int MaxLines = 30;

        void OnEnable()
        {
            if (MyDebug.IsDebugBuildOrEditor)
            {
                Application.logMessageReceived += HandleLogMessage;
            }
        }

        void OnDisable()
        {
            if (MyDebug.IsDebugBuildOrEditor)
            {
                Application.logMessageReceived -= HandleLogMessage;
            }
        }

        void HandleLogMessage(string logString, string stackTrace, LogType type)
        {
            Output = logString;
            Stack = stackTrace;
            string newString = "\n [" + type + "] : " + Output;
            LogMessagesQueue.Enqueue(newString);
            if (type == LogType.Exception)
            {
                newString = "\n" + stackTrace;
                LogMessagesQueue.Enqueue(newString);
            }

            while (LogMessagesQueue.Count > MaxLines)
            {
                LogMessagesQueue.Dequeue();
            }

            _myLog = string.Empty;
            foreach (string s in LogMessagesQueue)
            {
                _myLog += s;
            }
        }

        void OnGUI()
        {
            if (MyDebug.IsDebugBuildOrEditor)
            {
                if (_hidden)
                {
                    if (GUI.Button(new Rect(Screen.width - 100, 10, 80, 20), "Show"))
                    {
                        Hide(false);
                    }
                }
                else
                {
                    GUI.TextArea(new Rect(0, 0, (float)Screen.width / 3, Screen.height), _myLog);
                    if (GUI.Button(new Rect(Screen.width - 100, 10, 80, 20), "Hide"))
                    {
                        Hide(true);
                    }
                }
            }
        }

        public void Hide(bool shouldHide)
        {
            _hidden = shouldHide;
        }
    }
}