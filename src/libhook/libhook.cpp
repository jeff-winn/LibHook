#pragma once

#include "stdafx.h"
#include "libhook.h"

HMODULE g_hModule;

HHOOK g_ShellHook = NULL;
HookProc ShellHookCallback = NULL;
static LRESULT CALLBACK InternalShellHookCallback(int nCode, WPARAM wParam, LPARAM lParam);

HHOOK g_KeyboardLLHook = NULL;
HookProc KeyboardLLHookCallback = NULL;
static LRESULT CALLBACK InternalKeyboardLLHookCallback(int nCode, WPARAM wParam, LPARAM lParam);

HHOOK g_MouseLLHook = NULL;
HookProc MouseLLHookCallback = NULL;
static LRESULT CALLBACK InternalMouseLLHookCallback(int nCode, WPARAM wParam, LPARAM lParam);

HHOOK InitHook(int idHook, HookProc lpfn)
{	
	HHOOK result = NULL;

	switch (idHook)
	{
	case WH_SHELL:
		if (g_ShellHook != NULL)
		{
			UnhookWindowsHookEx(g_ShellHook);
		}

		g_ShellHook = SetWindowsHookEx(WH_SHELL, InternalShellHookCallback, g_hModule, 0);
		ShellHookCallback = lpfn;

		result = g_ShellHook;
		break;
		
	case WH_KEYBOARD_LL:
		if (g_KeyboardLLHook != NULL)
		{
			UnhookWindowsHookEx(g_KeyboardLLHook);
		}

		g_KeyboardLLHook = SetWindowsHookEx(WH_KEYBOARD_LL, InternalKeyboardLLHookCallback, g_hModule, 0);
		KeyboardLLHookCallback = lpfn;
		
		result = g_KeyboardLLHook;
		break;

	case WH_MOUSE_LL:
		if (g_MouseLLHook != NULL)
		{
			UnhookWindowsHookEx(g_MouseLLHook);
		}

		g_MouseLLHook = SetWindowsHookEx(WH_MOUSE_LL, InternalMouseLLHookCallback, g_hModule, 0);
		MouseLLHookCallback = lpfn;

		result = g_MouseLLHook;
		break;
	}

	return result;
}

BOOL ReleaseHook(HHOOK hhk)
{
	if (hhk == FALSE)
	{
		return FALSE;
	}

	HHOOK* pHook = NULL;
	HookProc* proc = NULL;

	if (hhk == g_ShellHook)
	{
		pHook = &g_ShellHook;
		proc = &ShellHookCallback;
	}
	else if (hhk == g_KeyboardLLHook)
	{
		pHook = &g_KeyboardLLHook;
		proc = &KeyboardLLHookCallback;
	}
	else if (hhk == g_MouseLLHook)
	{
		pHook = &g_MouseLLHook;
		proc = &MouseLLHookCallback;
	}

	if (pHook != NULL)
	{
		BOOL result = UnhookWindowsHookEx((HHOOK)pHook);
		if (result == TRUE)
		{
			pHook = NULL;
			proc = NULL;
		}

		return result;
	}

	return FALSE;
}

static LRESULT CALLBACK InternalKeyboardLLHookCallback(int nCode, WPARAM wParam, LPARAM lParam)
{
	return InternalHandleCallback(g_KeyboardLLHook, KeyboardLLHookCallback, nCode, wParam, lParam);
}

static LRESULT CALLBACK InternalShellHookCallback(int nCode, WPARAM wParam, LPARAM lParam)
{
	return InternalHandleCallback(g_ShellHook, ShellHookCallback, nCode, wParam, lParam);
}

static LRESULT CALLBACK InternalMouseLLHookCallback(int nCode, WPARAM wParam, LPARAM lParam)
{
	return InternalHandleCallback(g_MouseLLHook, MouseLLHookCallback, nCode, wParam, lParam);
}

static LRESULT InternalHandleCallback(HHOOK hhk, HookProc callback, int nCode, WPARAM wParam, LPARAM lParam)
{
	if (nCode >= 0 && callback != NULL)
	{
		LRESULT result = callback(nCode, wParam, lParam);
		if (result == TRUE)
		{
			return TRUE;
		}
	}

	return CallNextHookEx(hhk, nCode, wParam, lParam);
}

short GetAppCommand(LPARAM lParam)
{
	return GET_APPCOMMAND_LPARAM(lParam);
}

short GetDevice(LPARAM lParam)
{
	return GET_DEVICE_LPARAM(lParam);
}

short GetKeyState(LPARAM lParam)
{
	return GET_KEYSTATE_LPARAM(lParam);
}