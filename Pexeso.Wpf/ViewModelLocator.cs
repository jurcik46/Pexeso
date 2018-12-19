using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using Pexeso.Wpf.Interfaces;
using Pexeso.Wpf.Services;
using Pexeso.Wpf.ViewModels;

namespace Pexeso.Wpf
{
    public static class ViewModelLocator
    {

        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            //if (ViewModelBase.IsInDesignModeStatic)
            //{
            //    // Create design time view services and models
            //    //SimpleIoc.Default.Register<ITestService, DesignTestService>();
            //    SimpleIoc.Default.Register<IVideoService, VideoService>();

            //}
            //else
            //{
            //    // Create run time view services and models
            //    //SimpleIoc.Default.Register<ITestService, TestService>();
            SimpleIoc.Default.Register<IPexesoService, PexesoService>();
            SimpleIoc.Default.Register<IChatService, ChatService>();

            //}

            RegisterViewModels();

        }

        internal static void RegisterViewModels()
        {
            if (!SimpleIoc.Default.IsRegistered<MainViewModel>())
            {
                SimpleIoc.Default.Register<MainViewModel>();
            }

            if (!SimpleIoc.Default.IsRegistered<LoginViewModel>())
            {
                SimpleIoc.Default.Register<LoginViewModel>();
            }
        }



        public static MainViewModel MainViewModel => ServiceLocator.Current.GetInstance<MainViewModel>();
        public static IPexesoService PexesoService => ServiceLocator.Current.GetInstance<IPexesoService>();
        public static IChatService ChatService => ServiceLocator.Current.GetInstance<IChatService>();
        public static LoginViewModel LoginViewModel => ServiceLocator.Current.GetInstance<LoginViewModel>();




    }
}
