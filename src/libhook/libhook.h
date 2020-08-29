#pragma once

typedef LRESULT (CALLBACK *HookProc)(int nCode, WPARAM wParam, LPARAM lParam);

HHOOK InitHook(int idHook, HookProc lpfn, DWORD dwThreadId);
BOOL ReleaseHook(HHOOK hhk);
static LRESULT InternalHandleCallback(HHOOK hhk, HookProc callback, int nCode, WPARAM wParam, LPARAM lParam);

short GetAppCommand(LPARAM lParam);
short GetDevice(LPARAM lParam);
short GetKeyState(LPARAM lParam);