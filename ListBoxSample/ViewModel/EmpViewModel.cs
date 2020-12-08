namespace ListBoxSample.ViewModel
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Windows;
    using Model;
    using Command;
    using System.Windows.Input;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Text;

    internal class EmpVlewModel : ObservableCollection<Emp>, INotifyPropertyChanged
    {
        //ListView
        public ListCollectionView MyCollectionView;
        // ??? 이해가 안됨...
        public Emp emp { get; set; }
        public EmpVlewModel()
        {
            MyCollectionView = (ListCollectionView)CollectionViewSource.GetDefaultView(this);

            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
                // XAML 디자인을 위해서 넣은 값들
                // 디자인 시에만 표시됨
                Add(new Emp() { Empno = 1, Ename = "김길동", Job = "Salesman" });
                Add(new Emp() { Empno = 2, Ename = "박길동", Job = "Clerk" });
                Add(new Emp() { Empno = 3, Ename = "정길동", Job = "Clerk" });
                Add(new Emp() { Empno = 4, Ename = "남길동", Job = "Clerk" });
                Add(new Emp() { Empno = 5, Ename = "황길동", Job = "Salesman" });
                Add(new Emp() { Empno = 6, Ename = "홍길동", Job = "Manager" });
            }
            else
            {
                // 정상 실행됬을 경우
                Add(new Emp() { Empno = 1, Ename = "김길동", Job = "Salesman" });
                Add(new Emp() { Empno = 2, Ename = "박길동", Job = "Clerk" });
                Add(new Emp() { Empno = 3, Ename = "정길동", Job = "Clerk" });
                Add(new Emp() { Empno = 4, Ename = "남길동", Job = "Clerk" });
                Add(new Emp() { Empno = 5, Ename = "황길동", Job = "Salesman" });
                Add(new Emp() { Empno = 6, Ename = "홍길동", Job = "Manager" });
            }

            
            // Commands
            WindowLoadedCommand = new RelayCommand<object>(WindowLoaded);
            DCChanged = new RelayCommand<StackPanel>(OnDCChanged);
            ClickComand = new RelayCommand<Button>(OnClick);
            MoveCommand = new RelayCommand<Button>(OnMove);
            FilterCommand = new RelayCommand<Button>(OnFilter);
        }


        #region OnCommand 
        // Double Clicked Show selected Item
        private void WindowLoaded(object sender)
        {
            var Listbox = sender as ListBox;
            var item = Listbox.Items.CurrentItem as Emp;

            if (item == null)
                return;

            StringBuilder sb = new StringBuilder("Clieked ", 50);
            sb.AppendFormat("{0},{1},{2}", item.Empno.ToString(), item.Ename, item.Job);
            MessageBox.Show(sb.ToString());
        }
        private void OnDCChanged(StackPanel panel)
        {
          //  MyCollectionView = (ListCollectionView)CollectionViewSource.GetDefaultView(panel.DataContext);
        }

        private void OnClick(Button sender)
        {
            if (sender == null)
                return;

            MyCollectionView.SortDescriptions.Clear();

            switch (sender.Name)
            {
                case "BtnEmpno":
                    MyCollectionView.SortDescriptions.Add(new SortDescription("Empno", ListSortDirection.Ascending));
                    break;
                case "BtnEname":
                    MyCollectionView.SortDescriptions.Add(new SortDescription("Ename", ListSortDirection.Ascending));
                    break;
                case "BtnJob":
                    MyCollectionView.SortDescriptions.Add(new SortDescription("Job", ListSortDirection.Ascending));
                    break;
            }

        }

        //Prevous, Next 버튼 처리
        private void OnMove(Button sender)
        {
            if (sender == null)
                return;

            var b = sender as Button;

            switch (b.Name)
            {
                case "Previous":
                    if (MyCollectionView.MoveCurrentToPrevious())
                        emp = MyCollectionView.CurrentAddItem as Emp;
                    else
                        MyCollectionView.MoveCurrentToFirst();
                    break;
                case "Next":
                    if (MyCollectionView.MoveCurrentToNext())
                        emp = MyCollectionView.CurrentAddItem as Emp;
                    else
                        MyCollectionView.MoveCurrentToLast();
                    break;
            }
        }

        private void OnFilter(Button sender)
        {
            //토글 기능 구현
            switch (MyCollectionView.Filter)
            {
                //관리자만
                case null: MyCollectionView.Filter = IsManager; break;
                //전체사원
                default: MyCollectionView.Filter = null; break;
            }
        }

        private bool IsManager(object o)
        {
            var e = o as Emp;
            return e?.Job == "Manager";
        }

        #endregion

        // Commands
        #region ICommand Button


        public ICommand WindowLoadedCommand { get; private set; }
        public ICommand DCChanged { get; private set; }
        public ICommand ClickComand { get; private set; }
        public ICommand MoveCommand { get; private set; }
        public ICommand FilterCommand { get; private set; }

        #endregion
    }
}
