using System.ComponentModel.DataAnnotations;

namespace aegis_server.Models;

using System;
using System.Threading.Tasks;

public class Enemy
{
    public int Health { get; set; } = 50;
    public event Action OnAttack;

    public async void StartAttacking()
    {
        while (Health > 0)
        {
            await Task.Delay(2000); // Ataca a cada 2 segundos
            OnAttack?.Invoke();
        }
    }
}
