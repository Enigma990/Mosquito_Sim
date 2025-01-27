//using GooglePlayGames;
//using GooglePlayGames.BasicApi;
//using System.Collections;
//using System.Collections.Generic;
//using System.Security.Authentication;
//using System.Threading.Tasks;
//using UnityEngine;

//public class GooglePlayGamesManager : MonoBehaviour
//{
//    public string Token;
//    public string Error;

//    private void Awake()
//    {
//#if !UNITY_STANDALONE_WIN

//        PlayGamesPlatform.Activate();
//        LoginGooglePlayGames();

//#endif
//    }

//    public void LoginGooglePlayGames()
//    {

//#if !UNITY_STANDALONE_WIN

//        PlayGamesPlatform.Instance.Authenticate((success) =>
//        {
//            if (success == SignInStatus.Success)
//            {
//                Debug.Log("Login with Google Play games successful.");

//                PlayGamesPlatform.Instance.RequestServerSideAccess(true, code =>
//                {
//                    Debug.Log("Authorization code: " + code);
//                    Token = code;
//                    // This token serves as an example to be used for SignInWithGooglePlayGames
//                });
//            }
//            else
//            {
//                Error = "Failed to retrieve Google play games authorization code";
//                Debug.Log("Login Unsuccessful");
//            }
//        });

//#endif
//    }

//    //async Task SignInWithGooglePlayGamesAsync(string authCode)
//    //{
//    //    try
//    //    {
//    //        await AuthenticationService.Instance.SignInWithGooglePlayGamesAsync(authCode);
//    //        Debug.Log("SignIn is successful.");
//    //    }
//    //    catch (AuthenticationException ex)
//    //    {
//    //        // Compare error code to AuthenticationErrorCodes
//    //        // Notify the player with the proper error message
//    //        Debug.LogException(ex);
//    //    }
//    //    catch (RequestFailedException ex)
//    //    {
//    //        // Compare error code to CommonErrorCodes
//    //        // Notify the player with the proper error message
//    //        Debug.LogException(ex);
//    //    }
//    //}
//}
