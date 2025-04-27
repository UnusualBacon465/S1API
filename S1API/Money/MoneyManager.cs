// REMOVE dynamic usage. Use the real FloatContainer.

using S1API.Internal.Abstraction;
using UnityEngine;

namespace S1API.Money
{
    /// <summary>
    /// Provides methods for managing financial data, including cash balance,
    /// online transactions, and net worth calculations.
    /// </summary>
    public class MoneyManager : Registerable
    {
#if IL2CPP
        private static Il2CppScheduleOne.Money.MoneyManager InternalInstance => Il2CppScheduleOne.Money.MoneyManager.Instance;
#else
        /// <summary>
        /// Provides internal access to the singleton instance of the underlying `ScheduleOne.Money.MoneyManager`.
        /// This property is used internally to interact with the core money management system.
        /// </summary>
        private static ScheduleOne.Money.MoneyManager InternalInstance => ScheduleOne.Money.MoneyManager.Instance;
#endif

        /// <summary>
        /// Invoked when the instance of the class is created and initialized.
        /// Provides an entry point for subclasses to include additional setup logic during creation.
        /// </summary>
        protected override void OnCreated() => base.OnCreated();

        /// <summary>
        /// Executes cleanup or teardown logic when the instance is being destroyed.
        /// This method is invoked when the object lifecycle ends and should typically
        /// handle resource deallocation, event unsubscriptions, or other finalization tasks.
        /// </summary>
        protected override void OnDestroyed() => base.OnDestroyed();

        /// <summary>
        /// Changes the cash balance by the specified amount.
        /// </summary>
        /// <param name="amount">The amount to adjust the cash balance by. Positive values increase the balance, while negative values decrease it.</param>
        /// <param name="visualizeChange">Indicates whether the change in cash balance should be visually represented.</param>
        /// <param name="playCashSound">Indicates whether a sound effect should play when the cash balance is changed.</param>
        public static void ChangeCashBalance(float amount, bool visualizeChange = true, bool playCashSound = false)
        {
            InternalInstance?.ChangeCashBalance(amount, visualizeChange, playCashSound);
        }

        /// <summary>
        /// Creates an online transaction with specified details.
        /// </summary>
        /// <param name="transactionName">The name of the transaction.</param>
        /// <param name="unitAmount">The monetary value of a single unit in the transaction.</param>
        /// <param name="quantity">The quantity involved in the transaction.</param>
        /// <param name="transactionNote">A note or description for the transaction.</param>
        public static void CreateOnlineTransaction(string transactionName, float unitAmount, float quantity, string transactionNote)
        {
            InternalInstance?.CreateOnlineTransaction(transactionName, unitAmount, quantity, transactionNote);
        }

        /// <summary>
        /// Retrieves the current net worth.
        /// </summary>
        /// <returns>The calculated net worth as a floating-point number.</returns>
        public static float GetNetWorth()
        {
            return InternalInstance != null ? InternalInstance.GetNetWorth() : 0f;
        }

        /// <summary>
        /// Retrieves the current cash balance.
        /// </summary>
        /// <returns>The current cash balance as a floating-point value.</returns>
        public static float GetCashBalance()
        {
            return InternalInstance != null ? InternalInstance.cashBalance : 0f;
        }

        /// <summary>
        /// Retrieves the current online balance of the user.
        /// </summary>
        /// <returns>
        /// A float representing the user's current online balance. Returns 0 if the internal instance is null.
        /// </returns>
        public static float GetOnlineBalance()
        {
            return InternalInstance != null ? InternalInstance.sync___get_value_onlineBalance() : 0f;
        }

        /// <summary>
        /// Register a networth calculation event.
        /// </summary>
        /// <param name="callback">An action to invoke during the networth calculation event.</param>
        public static void AddNetworthCalculation(System.Action<object> callback)
        {
            if (InternalInstance != null)
                InternalInstance.onNetworthCalculation += callback;
        }

        /// <summary>
        /// Remove a networth calculation event.
        /// </summary>
        /// <param name="callback">The callback function to remove from the networth calculation event.</param>
        public static void RemoveNetworthCalculation(System.Action<object> callback)
        {
            if (InternalInstance != null)
                InternalInstance.onNetworthCalculation -= callback;
        }
    }
}
