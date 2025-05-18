using System;
using System.Collections.Generic;

namespace BankPelita
{
    class Nasabah
    {
        public string NomorRekening { get; set; }
        public string Nama { get; set; }
        public double Saldo { get; private set; }

        public Nasabah(string nomorRekening, string nama, double saldoAwal)
        {
            NomorRekening = nomorRekening;
            Nama = nama;
            Saldo = saldoAwal;
        }

        public void Setor(double jumlah)
        {
            Saldo += jumlah;
            Console.WriteLine($"Setor Tunai berhasil. \nSaldo anda: Rp{Saldo}");
        }

        public void Tarik(double jumlah)
        {
            if (jumlah > Saldo)
            {
                Console.WriteLine("Saldo anda tidak mencukupi.");
            }
            else
            {
                Saldo -= jumlah;
                Console.WriteLine($"Penarikan dana berhasil. \nSaldo anda: Rp{Saldo}");
            }
        }

        public void Transfer(Nasabah penerima, double jumlah)
        {
            if (jumlah > Saldo)
            {
                Console.WriteLine("Saldo tidak mencukupi untuk transfer.");
            }
            else
            {
                Saldo -= jumlah;
                penerima.Saldo += jumlah;
                Console.WriteLine($"Transfer berhasil ke {penerima.Nama}. \nSaldo Anda: Rp{Saldo}");
            }
        }

        public void TampilkanInfo()
        {
            Console.WriteLine("=== Data Rekening ===");
            Console.WriteLine($"Nomor Rekening : {NomorRekening}");
            Console.WriteLine($"Nama Pemilik   : {Nama}");
            Console.WriteLine($"Saldo Rekening : Rp{Saldo}");
        }
    }

    class Program
    {
        static List<Nasabah> daftarNasabah = new List<Nasabah>();

        static Nasabah CariNasabah(string norek)
        {
            foreach (var n in daftarNasabah)
            {
                if (n.NomorRekening == norek)
                    return n;
            }
            return null;
        }

        static void Main(string[] args)
        {
            daftarNasabah.Add(new Nasabah("111", "Varen", 500000));
            daftarNasabah.Add(new Nasabah("222", "Sopo", 300000));
            daftarNasabah.Add(new Nasabah("333", "Jarwo", 700000));

            while (true)
            {
                Console.WriteLine("\n=== BANK PELITA ===");
                Console.Write("Masukkan Nomor Rekening Anda: ");
                string norek = Console.ReadLine();
                Nasabah pengguna = CariNasabah(norek);

                if (pengguna == null)
                {
                    Console.WriteLine("Nomor rekening tidak ditemukan.");
                    continue;
                }

                Console.WriteLine($"\nSelamat datang, {pengguna.Nama}!");
                Console.WriteLine("1. Penarikan Dana");
                Console.WriteLine("2. Setor Tunai");
                Console.WriteLine("3. Transfer Antar Rekening");
                Console.WriteLine("4. Tampilkan Data Rekening");
                Console.WriteLine("5. Keluar");
                Console.Write("Pilih menu: ");
                string pilihan = Console.ReadLine();

                switch (pilihan)
                {
                    case "1":
                        Console.Write("Masukkan jumlah penarikan dana: ");
                        double tarik = Convert.ToDouble(Console.ReadLine());
                        pengguna.Tarik(tarik);
                        break;

                    case "2":
                        Console.Write("Masukkan jumlah setor tunai: ");
                        double setor = Convert.ToDouble(Console.ReadLine());
                        pengguna.Setor(setor);
                        break;

                    case "3":
                        Console.Write("Masukkan nomor rekening tujuan: ");
                        string norekTujuan = Console.ReadLine();
                        Nasabah penerima = CariNasabah(norekTujuan);
                        if (penerima == null)
                        {
                            Console.WriteLine("Rekening tujuan tidak ditemukan.");
                        }
                        else
                        {
                            Console.Write("Masukkan jumlah transfer: ");
                            double jumlah = Convert.ToDouble(Console.ReadLine());
                            pengguna.Transfer(penerima, jumlah);
                        }
                        break;

                    case "4":
                        pengguna.TampilkanInfo();
                        break;

                    case "5":
                        Console.WriteLine("Terima kasih telah menggunakan layanan Bank Pelita.");
                        return;

                    default:
                        Console.WriteLine("Pilihan tidak valid.");
                        break;
                }
            }
        }
    }
}
