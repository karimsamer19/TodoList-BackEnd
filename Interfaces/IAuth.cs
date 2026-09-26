using System;
using TodoList.DTOs;

namespace TodoList.Interfaces;

public interface IAuth
{
    Task<bool> Register(RegisterDto dto);
    Task<string?> Login(LoginDto dto);
}
