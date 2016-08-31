using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.Purchasing;

// Placing the Purchaser class in the CompleteProject namespace allows it to interact with ScoreManager, 
// one of the existing Survival Shooter scripts.

// Deriving the Purchaser class from IStoreListener enables it to receive messages from Unity Purchasing.
public class InappManager : MonoBehaviour, IStoreListener
{
    public static InappManager Instance { set; get; }

    private static IStoreController m_StoreController;          // The Unity Purchasing system.
    private static IExtensionProvider m_StoreExtensionProvider; // The store-specific Purchasing subsystems.

    public static string Product_20000Bosuk = "gold20000";
    public static string Product_30000Bosuk ="gold30000";
    public static string Product_100000Bosuk = "gold100000";
    
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        InitializePurchasing();
    }

    public void InitializePurchasing()
    {
        if (IsInitialized())
        {
            return;
        }
        
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        
       
        builder.AddProduct(Product_100000Bosuk, ProductType.Consumable);
        builder.AddProduct(Product_30000Bosuk, ProductType.Consumable);
        builder.AddProduct(Product_20000Bosuk, ProductType.Consumable);

        UnityPurchasing.Initialize(this, builder);
    }


    private bool IsInitialized()
    {
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }


    public void Buy20000Bosuk()
    {
        BuyProductID(Product_20000Bosuk);
    }
    public void Buy30000Bosuk()
    {
        BuyProductID(Product_30000Bosuk);
    }
    public void Buy100000Bosuk()
    {
        BuyProductID(Product_100000Bosuk);
    }
    /*
    public void BuyNonConsumable()
    {
        // Buy the non-consumable product using its general identifier. Expect a response either 
        // through ProcessPurchase or OnPurchaseFailed asynchronously.
        BuyProductID(kProductIDNonConsumable);
    }*/

    /*
    public void BuySubscription()
    {
        // Buy the subscription product using its the general identifier. Expect a response either 
        // through ProcessPurchase or OnPurchaseFailed asynchronously.
        // Notice how we use the general product identifier in spite of this ID being mapped to
        // custom store-specific identifiers above.
        BuyProductID(kProductIDSubscription);
    }*/


    void BuyProductID(string productId)
    {
        if (IsInitialized())
        {
            Product product = m_StoreController.products.WithID(productId);
            
            if (product != null && product.availableToPurchase)
            {
                Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                m_StoreController.InitiatePurchase(product);
            }
            else
            {
                Debug.Log("상품을 찾을 수가 없습니다.");
                Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
            }
        }
        else
        {
            Debug.Log("BuyProductID FAIL. Not initialized.");
        }
    }

    
    public void RestorePurchases()
    {
        if (!IsInitialized())
        {
            Debug.Log("RestorePurchases FAIL. Not initialized.");
            return;
        }
        
        if (Application.platform == RuntimePlatform.IPhonePlayer ||
            Application.platform == RuntimePlatform.OSXPlayer)
        {
            Debug.Log("RestorePurchases started ...");
            
            var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
            apple.RestoreTransactions((result) =>
            {
                Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
            });
        }
        else
        {
            Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
        }
    }


    //  
    // --- IStoreListener
    //

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        Debug.Log("OnInitialized: PASS");

        // Overall Purchasing system, configured with products for this application.
        m_StoreController = controller;
        // Store specific subsystem, for accessing device-specific store features.
        m_StoreExtensionProvider = extensions;
    }


    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
    }


    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        if (String.Equals(args.purchasedProduct.definition.id, Product_20000Bosuk, StringComparison.Ordinal))
        {
            Debug.Log("you 20000 보석!"); //돈이 올라가는곳
            DataSave._instance.setBosuk_Game(20000);
        }
        else if (String.Equals(args.purchasedProduct.definition.id, Product_30000Bosuk, StringComparison.Ordinal))
        {
            Debug.Log("you 30000 보석");
            DataSave._instance.setBosuk_Game(30000);
        }
        else if (String.Equals(args.purchasedProduct.definition.id, Product_100000Bosuk, StringComparison.Ordinal))
        {
            Debug.Log("you 100000 보석");
            DataSave._instance.setBosuk_Game(100000);
        }
        /*else if (String.Equals(args.purchasedProduct.definition.id, kProductIDSubscription, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            // TODO: The subscription item has been successfully purchased, grant this to the player.
        }*/
        // Or ... an unknown product has been purchased by this user. Fill in additional products here....
        else
        {
            Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
        }

        // Return a flag indicating whether this product has completely been received, or if the application needs 
        // to be reminded of this purchase at next app launch. Use PurchaseProcessingResult.Pending when still 
        // saving purchased products to the cloud, and when that save is delayed. 
        return PurchaseProcessingResult.Complete;
    }


    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
    }
}
