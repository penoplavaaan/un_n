static void UpdateNumber(int n)
{
    n++;
}

static void UpdateArray1(int[] a)
{
    a[0]++;
}

static void UpdateArray2(int[] a)
{
    a = new int[3];
    a[0]++;
}

static void Main()
{
    int n;
    n = 1;
    UpdateNumber(n);
    Console.WriteLine(n);

    int[] a = new int[3];
    UpdateArray1(a);
    Console.WriteLine(a[0]);

    a = new int[3];
    UpdateArray2(a);
    Console.WriteLine(a[0]);

}