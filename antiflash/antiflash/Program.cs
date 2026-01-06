using Swed64;


// init swed

Swed swed = new Swed("cs2");

// get client.dll

IntPtr client = swed.GetModuleBase("client.dll");

// now some offsets

int dwLocalPlayerPawn = 0x1BEEF28;
int m_flFlashBangTime = 0x15FC;

// anti flash loop

while (true) // always run
{
    // current data
    IntPtr localPlayerPawn = swed.ReadPointer(client, dwLocalPlayerPawn);

    float flashDuration = swed.ReadFloat(localPlayerPawn, m_flFlashBangTime); // 0 -> 1

    if (flashDuration > 0)
    {
        swed.WriteFloat(localPlayerPawn, m_flFlashBangTime, 0); // remove flash
        Console.WriteLine("evaded flash!");
    }
    Thread.Sleep(2); // let cpu rest
}