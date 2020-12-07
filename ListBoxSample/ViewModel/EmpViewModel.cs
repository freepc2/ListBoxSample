namespace ListBoxSample.ViewModel
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Windows;
    using Model;
    using Command;
    using System.Windows.Input;

    class EmpVlewModel : ObservableCollection<Emp>
    {
        public EmpVlewModel()
        {
            Add(new Emp() { Empno = 1, Ename = "김길동", Job = "Salesman" });
            Add(new Emp() { Empno = 2, Ename = "박길동", Job = "Clerk" });
            Add(new Emp() { Empno = 3, Ename = "정길동", Job = "Clerk" });
            Add(new Emp() { Empno = 4, Ename = "남길동", Job = "Clerk" });
            Add(new Emp() { Empno = 5, Ename = "황길동", Job = "Salesman" });
            Add(new Emp() { Empno = 6, Ename = "홍길동", Job = "Manager" });
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
                // XAML 디자인을 위해서 넣은 값들
                // 디자인 시에만 표시됨

            }
            else
            {
                // 정상 실행됬을 경우
            }

            WindowLoadedCommand = new RelayCommand(WindowLoaded);
        }
        private void WindowLoaded()
        {
            MessageBox.Show("WindowLoaded");
        }
        public ICommand WindowLoadedCommand { get; private set; }
    }
}
