using System;
using System.Runtime.InteropServices;
using System.Threading;
//menghapus using System.Timers;
namespace Formulatrix.Intern.GrabTheFrame;

public interface IFrameCallback
{
    // hapus public dari void
    void FrameReceived(IntPtr pFrame, int pixelWidth, int pixelHeight, int bytesPerPixel); // menambah bytesPerpixel
}

public interface IValueReporter
{
    void Report(double value); // hapus public dari void
}

public class FrameCalculateAndStream : IFrameCallback // inheritance
{
    private readonly IValueReporter _reporter;
    private readonly object _lock = new object(); // menggunakan readonly
    private byte[] _buffer; // mengganti Timer _timer 

    public FrameCalculateAndStream(IValueReporter reporter)
    {
        //menghapus timer
        _reporter = reporter;
    }

    public void FrameReceived(IntPtr pFrame, int pixelWidth, int pixelHeight, int bytesPerPixel)
    {
        int bufferSize = pixelWidth * pixelHeight * bytesPerPixel;

        // Inisialisasi atau ubah ukuran buffer jika perlu
        if (_buffer == null || _buffer.Length != bufferSize)
        {
            _buffer = new byte[bufferSize];
        }

        // Pastikan pointer tidak null
        if (pFrame == IntPtr.Zero)
        {
            Console.WriteLine("Invalid frame pointer received.");
            return;
        }

        // Salin data frame dari memori unmanaged ke managed memory
        Marshal.Copy(pFrame, _buffer, 0, bufferSize);

        // Hitung nilai rata-rata piksel
        double sum = 0;
        for (int i = 0; i < bufferSize; i++)
        {
            sum += _buffer[i];
        }

        double average = sum / bufferSize;

        // Laporkan nilai yang telah dihitung
        lock (_lock)
        {
            _reporter.Report(average);
        }
    }
}

// Komponen eksternal yang disimulasikan
public class ValueReporter : IValueReporter
{
    public void Report(double value)
    {
        Console.WriteLine($"Nilai rata-rata piksel: {value}");
    }
}

// Penggunaan
public class Program
{
    public static void Main()
    {
        // Thread.Sleep(10);
        var reporter = new ValueReporter();
        var frameProcessor = new FrameCalculateAndStream(reporter);

        // Simulasikan menerima frame dari kamera
        IntPtr simulatedFramePointer = Marshal.AllocHGlobal(640 * 480); // Pointer frame yang disimulasikan
        frameProcessor.FrameReceived(simulatedFramePointer, 640, 480, 1); // Diasumsikan 1 byte per piksel untuk grayscale

        Marshal.FreeHGlobal(simulatedFramePointer);
    }
}
