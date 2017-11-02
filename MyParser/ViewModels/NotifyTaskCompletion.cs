using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyParser.ViewModels
{
    sealed class NotifyTaskCompletion<TResult> : INotifyPropertyChanged
    {
        private readonly Task<TResult> task;

        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsComplete => task.IsCompleted;
        public TResult Result => CompletedSuccessfully ? task.Result : default(TResult);
        public TaskStatus Status => task.Status;
        public bool CompletedSuccessfully => task.Status == TaskStatus.RanToCompletion;
        public bool IsCancelled => task.IsCanceled;
        public bool IsFaulted => task.IsFaulted;
        public AggregateException Exception => task.Exception;
        public Exception InnerException => Exception.InnerException;
        public string ErrorMessage => InnerException?.Message;

        public NotifyTaskCompletion(Task<TResult> task)
        {
            this.task = task;

            if (!task.IsCompleted)
            {
                var _ = RunTask(task);
            }
        }

        private async Task RunTask(Task<TResult> task)
        {
            try
            {
                await task;
            }
            catch
            {
                // we'll ask the task object if anything went wrong
            }

            var propertyChanged = PropertyChanged;
            if (propertyChanged != null)
            {
                propertyChanged(this, new PropertyChangedEventArgs(nameof(IsComplete)));
                propertyChanged(this, new PropertyChangedEventArgs(nameof(Status)));
                if (IsCancelled)
                {
                    propertyChanged(this, new PropertyChangedEventArgs(nameof(IsCancelled)));
                }
                else if (IsFaulted)
                {
                    propertyChanged(this, new PropertyChangedEventArgs(nameof(Exception)));
                    propertyChanged(this, new PropertyChangedEventArgs(nameof(InnerException)));
                    propertyChanged(this, new PropertyChangedEventArgs(nameof(ErrorMessage)));
                }
                else
                {
                    propertyChanged(this, new PropertyChangedEventArgs(nameof(Result)));
                    propertyChanged(this, new PropertyChangedEventArgs(nameof(CompletedSuccessfully)));
                }
            }
        }
    }
}
