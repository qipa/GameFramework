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

using FlipWebApps.GameFramework.Scripts.Debugging;
using UnityEngine;

namespace FlipWebApps.GameFramework.Scripts.GameObjects.Components
{
    /// <summary>
    ///  A singleton implementation pattern
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        // Static singleton property
        public static T Instance { get; private set; }
        public string TypeName { get; private set; }

        public static bool IsActive
        {
            get
            {
                return Instance != null;
            }
        }

        void Awake()
        {
            TypeName = typeof(T).FullName;
            MyDebug.Log(TypeName + ": Awake");

            // First we check if there are any other instances conflicting then destroy this and return
            if (Instance != null)
            {
                if (Instance != this)
                    Destroy(gameObject);
                return;             // return is my addition so that the inspector in unity still updates
            }

            // Here we save our singleton instance
            Instance = this as T;

            // setup specifics.
            GameSetup();
        }

        void OnDestroy()
        {
            MyDebug.Log(TypeName + ": OnDestroy");

            if (Instance == this)
                GameDestroy();
        }

        protected virtual void GameSetup()
        {
        }

        protected virtual void GameDestroy()
        {
        }
    }
}