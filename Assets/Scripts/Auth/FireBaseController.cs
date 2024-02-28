//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using TMPro;
//using System;
//using Firebase;
//using Firebase.Auth;
//using System.Threading.Tasks;
//using Firebase.Extensions;

//public class FirebaseController : MonoBehaviour
//{

//    [SerializeField] GameObject loginPanel, signupPanel, profilePanel, forgetPasswordPanel, notificationPanel;

//    [SerializeField] TMP_InputField loginEmail, loginPassword, signupEmail, signupPassword, signupCPassword, signupUser, forgetPassEmail;

//    [SerializeField] TMP_Text notif_Title_Text, notif_Message_Text , profileUser , profileEmail ;

//    [SerializeField] Toggle RemmeberMe;
//    Firebase.Auth.FirebaseAuth auth;
//    Firebase.Auth.FirebaseUser user;
//    bool isSignIn = false;


//     void Start()
//    {
//        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
//            var dependencyStatus = task.Result;
//            if (dependencyStatus == Firebase.DependencyStatus.Available)
//            {
//                // Create and hold a reference to your FirebaseApp,
//                // where app is a Firebase.FirebaseApp property of your application class.
//                InitializeFirebase();
//                // Set a flag here to indicate whether Firebase is ready to use by your app.
//            }
//            else
//            {
//                UnityEngine.Debug.LogError(System.String.Format(
//                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
//                // Firebase Unity SDK is not safe to use here.
//            }
//        });

//    }
//    public void OpenLoginPanel()
//    {

//        loginPanel.SetActive(true);

//        signupPanel.SetActive(false);

//        profilePanel.SetActive(false);

//        forgetPasswordPanel.SetActive(false);
//    }

//    public void OpenSignUpPanel()
//    {
//        loginPanel.SetActive(false);

//        signupPanel.SetActive(true);

//        profilePanel.SetActive(false);

//        forgetPasswordPanel.SetActive(false);
//    }


//    public void OpenProfilePanel()
//    {
//        loginPanel.SetActive(false);

//        signupPanel.SetActive(false);

//        profilePanel.SetActive(true);

//        forgetPasswordPanel.SetActive(false);
//    }

//    public void OpenforgetPasswordPanel()
//    {
//        loginPanel.SetActive(false);

//        signupPanel.SetActive(false);

//        profilePanel.SetActive(false);

//        forgetPasswordPanel.SetActive(true);
//    }

//    public void LoginUser()
//    {
//        if (string.IsNullOrEmpty(loginEmail.text) || string.IsNullOrEmpty(loginPassword.text))
//        {
//            showNotificationMessage("Error", "Fields empty ! "); 
//            return;
//        }
//        SigniExistingUsers(loginEmail.text, loginPassword.text);
//    }

//    public void SignUpUser()
//    {
//        if (string.IsNullOrEmpty(signupEmail.text) || string.IsNullOrEmpty(signupPassword.text) || string.IsNullOrEmpty(signupCPassword.text) || string.IsNullOrEmpty(signupUser.text))
//        {
//            showNotificationMessage("Error", "Fields empty ! ");
//            return;
//        }
//        CreateUser(signupEmail.text, signupPassword.text,signupUser.text);

//    }
//    public void ForgetPassword()
//    {
//        if (string.IsNullOrEmpty(forgetPassEmail.text))
//        {
//            showNotificationMessage("Error", "Fields empty ! ");
//            return;
//        }

//    }





//    public void showNotificationMessage(string title, string discription)
//    {
//        notif_Title_Text.text = "" + title;
//        notif_Message_Text.text = "" + discription;
//        notificationPanel.SetActive(true); 
//    }

//    public void closeNotificationMessage()
//    {
//        notif_Title_Text.text = "" ;
//        notif_Message_Text.text = "";
//        notificationPanel.SetActive(false);
//    }

//    public void logout()
//    {
//        auth.SignOut();
//        profileUser.text = "";
//        profileEmail.text = "";
//        OpenLoginPanel();
//    }

//    public void CreateUser(string email, string password,string username)
//    {
//        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task => {
//            if (task.IsCanceled)
//            {
//                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
//                return;
//            }
//            if (task.IsFaulted)
//            {
//                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
//                return;
//            }

//            // Firebase user has been created.
//            Firebase.Auth.AuthResult result = task.Result;
//            Debug.LogFormat("Firebase user created successfully: {0} ({1})",
//                result.User.DisplayName, result.User.UserId);
//        });
//        updateUserProfile(username);
//    }
//    public void SigniExistingUsers(string email, string password)
//    {
//        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
//            if (task.IsCanceled)
//            {
//                Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
//                return;
//            }
//            if (task.IsFaulted)
//            {
//                Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
//                return;
//            }

//            Firebase.Auth.AuthResult result = task.Result;
//            Debug.LogFormat("User signed in successfully: {0} ({1})",
//                result.User.DisplayName, result.User.UserId);
//        });
//        profileUser.text = "" + user.DisplayName;
//        profileEmail.text = "" + user.Email; 
//        OpenProfilePanel();
//    }

//    void InitializeFirebase()
//    {
//        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
//        auth.StateChanged += AuthStateChanged;
//        AuthStateChanged(this, null);
//    }

//    void AuthStateChanged(object sender, System.EventArgs eventArgs)
//    {
//        if (auth.CurrentUser != user)
//        {
//            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null
//                && auth.CurrentUser.IsValid();
//            if (!signedIn && user != null)
//            {
//                Debug.Log("Signed out " + user.UserId);
//            }
//            user = auth.CurrentUser;
//            if (signedIn)
//            {
//                Debug.Log("Signed in " + user.UserId);
//                isSignIn = true;
//            }
//        }
//    }

//    void OnDestroy()
//    {
//        auth.StateChanged -= AuthStateChanged;
//        auth = null;
//    }
//    void updateUserProfile(string username)
//    {

//        Firebase.Auth.FirebaseUser user = auth.CurrentUser;
//        if (user != null)
//        {
//            Firebase.Auth.UserProfile profile = new Firebase.Auth.UserProfile
//            {
//                DisplayName = username,
               
//            };
//            user.UpdateUserProfileAsync(profile).ContinueWith(task => {
//                if (task.IsCanceled)
//                {
//                    Debug.LogError("UpdateUserProfileAsync was canceled.");
//                    return;
//                }
//                if (task.IsFaulted)
//                {
//                    Debug.LogError("UpdateUserProfileAsync encountered an error: " + task.Exception);
//                    return;
//                }

//                Debug.Log("User profile updated successfully.");

//                showNotificationMessage("Alert", "account Created");
//            });
//        }
//    }

//    bool isSigned = false; 
//     void Update()
//    {
//            if (isSignIn)
//        {
//            if (!isSigned)
//            {
//                isSigned = true;
//                profileUser.text = "" + user.DisplayName;
//                profileEmail.text = "" + user.Email;
//                OpenProfilePanel();


//            }
//        }
//    }
//}