using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace Cyberpunk_2020_editor.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            // Создаем новый документ PDF
            PdfDocument document = new PdfDocument();
            if (!string.IsNullOrEmpty(charactername.Text))
                document.Info.Title = charactername.Text;
            else
            {
                document.Info.Title = "Cyberpunk character";
            }

            // Добавляем страницу формата A4
            PdfPage page = document.AddPage();
            page.Orientation = PdfSharp.PageOrientation.Portrait;
            page.Width = XUnit.FromMillimeter(210);
            page.Height = XUnit.FromMillimeter(297);

            XGraphics gfx = XGraphics.FromPdfPage(page);

            // Получаем изображение GroupBox в виде BitmapSource
            BitmapSource groupBoxBitmap = RenderGroupBoxToBitmap(page_one);

            // Рисуем изображение на странице PDF
            using (MemoryStream stream = new MemoryStream())
            {
                BitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(groupBoxBitmap));
                encoder.Save(stream);
                XImage image = XImage.FromStream(stream);
                gfx.DrawImage(image, 0, 0, page.Width, page.Height);
            }

            // Сохраняем документ PDF
            document.Save("page.pdf");
        }

        public BitmapSource RenderGroupBoxToBitmap(GroupBox groupBox)
        {
            // Устанавливаем размеры для рендера
            double width = page_one.ActualWidth * 300 / 96;
            double height = page_one.ActualHeight * 300 / 96;

            // Создаем RenderTargetBitmap с указанными размерами и DPI
            RenderTargetBitmap renderBitmap =
                new RenderTargetBitmap((int)width, (int)height, 96, 96, PixelFormats.Pbgra32);

            // Вызываем Measure и Arrange, чтобы GroupBox "подготовился" к рендерингу
            groupBox.Measure(new Size(width, height));
            groupBox.Arrange(new Rect(new Point(0, 0), new Size(width, height)));

            // Создаем DrawingVisual и отрисовываем содержимое GroupBox
            DrawingVisual drawingVisual = new DrawingVisual();
            using (DrawingContext drawingContext = drawingVisual.RenderOpen())
            {
                // Рисуем содержимое GroupBox
                drawingContext.DrawRectangle(new VisualBrush(groupBox), null,
                    new Rect(new Point(), new Size(width, height)));
            }

            // Рендерим DrawingVisual в RenderTargetBitmap
            renderBitmap.Render(drawingVisual);

            return renderBitmap;
        }

        /*    private void MinimizeButton_OnClick(object sender, RoutedEventArgs e)
            {
                App.Current.MainWindow.WindowState = WindowState.Minimized;
            }
    
            private void MaximizeButton_OnClick(object sender, RoutedEventArgs e)
            {
                maximize();
            }
    
            private void CloseButton_OnClick(object sender, RoutedEventArgs e)
            {
                Application.Current.MainWindow.Close();
            }
    
            private void maximize()
            {
                if (App.Current.MainWindow.WindowState == WindowState.Maximized)
                {
                    App.Current.MainWindow.WindowState = WindowState.Normal;
                }
                else if (App.Current.MainWindow.WindowState == WindowState.Normal)
                {
                    App.Current.MainWindow.WindowState = WindowState.Maximized;
                }
            }
    
            private void UIElement_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
            {
                if (e.ClickCount == 2)
                {
                    maximize();
                }
                else
                {
                    Application.Current.MainWindow.DragMove();
                }
            }*/
        private void AddPhoto_OnClick(object sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }
    }
}