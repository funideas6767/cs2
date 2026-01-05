using FovChanger;
using Swed64;

// init swed
Swed swed = new Swed("cs2");

// get client
IntPtr client = swed.GetModuleBase("client.dll");

// init menu render
Renderer renderer = new Renderer();
renderer.Start().Wait();

// get offsets

// offsets.cs
int dwLocalPlayerPawn = 0x1BEEF28;

// client.dll

int m_pCameraServices = 0x1428;
int m_iFOV = 0x288;
int m_bIsScoped = 0x2718;

// fov changer loop
while(true)
{
    uint desiredFov = (uint)renderer.fov;
    // get pawn
    IntPtr localPlayerPawn = swed.ReadPointer(client, dwLocalPlayerPawn);
    // get camera services
    IntPtr cameraServices = swed.ReadPointer(localPlayerPawn, m_pCameraServices);
    // current FOV
    uint currentFov = swed.ReadUInt(cameraServices + m_iFOV);
    // if scoped, we don't write
    bool isScoped = swed.ReadBool(localPlayerPawn, m_bIsScoped);

    if (!isScoped && currentFov != desiredFov) // if we
    {
        swed.WriteUInt(cameraServices + m_iFOV, desiredFov);
    }
}