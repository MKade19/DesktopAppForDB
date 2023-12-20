using System.Windows;

namespace BusStation.UI.Util
{
    public static class MessageBoxStore
    {
        /// <summary>
        /// Shows MessageBox for confirmation and provides its result.
        /// </summary>
        /// <param name="message">Message for MessageBox.</param>
        /// <returns>Result of MessageBox.</returns>
        public static MessageBoxResult Confirmation(string message)
        {
            return MessageBox.Show
            (
                message, 
                "Подтверждение",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
            );
        }

        /// <summary>
        /// Shows MessageBox for error and provides its result.
        /// </summary>
        /// <param name="message">Message for MessageBox.</param>
        /// <returns>Result of MessageBox.</returns>
        public static MessageBoxResult Error(string message)
        {
            return MessageBox.Show
            (
                message,
                "Ошибка",
                MessageBoxButton.OKCancel,
                MessageBoxImage.Error
            );
        }

        /// <summary>
        /// Shows MessageBox for warning and provides its result.
        /// </summary>
        /// <param name="message">Message for MessageBox.</param>
        /// <returns>Result of MessageBox.</returns>
        public static MessageBoxResult Warning(string message)
        {
            return MessageBox.Show
            (
                message,
                "Предупреждение",
                MessageBoxButton.OKCancel,
                MessageBoxImage.Warning
            );
        }
    }
}
