using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SOLID : MonoBehaviour
{
    class SingleResponsibilityPrinciple
    {
        /// <summary>
        /// Cada clase hace solo una cosa:
        /// </summary>
        public class PlayerHealth
        {
            //Se encarga de la vida del player
        }
        public class PlayerHUD
        {
            //Se encarga del HUD
        }

        public class EnemySpawner
        {

        }
        public class AttackPlayer
        {

        }
        public class CharacterController
        {
            public PlayerHealth health;
            //Acciones basicas del character
        }
    }
    class OpenClosedPrinciple
    {
        /// <summary>
        /// Las clases deben estar abiertas a extension, cerradas a modificacion.
        /// </summary>
        public abstract class Weapon
        {
            public abstract void Attack();
        }

        public class Sword : Weapon
        {
            public override void Attack()
            {
                Debug.Log("Ataque con espada");
            }
        }


        public class Player : MonoBehaviour
        {
            public Weapon weapon;

            void Update()
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    weapon.Attack();
                }
            }

        }
    }
    class LiskovSubstitutionPrinciple
    {
        /// <summary>
        /// Las subclases deben poder reemplazar a sus clases base sin romper el codigo.
        /// </summary>
        public abstract class Enemy
        {
            public abstract void Attack();
        }

        public class Zombie : Enemy
        {
            public override void Attack()
            {
                Debug.Log("El zombie ataca");
            }
        }

        public class Boss : Enemy
        {
            public override void Attack()
            {
                Debug.Log("El jefe ataca");
            }
        }

        public class Game : MonoBehaviour
        {
            Enemy boss;
            Enemy zombie;
            void Start()
            {
                // Funciona igual que con cualquier Enemy
                boss.Attack(); 
                zombie.Attack();
            }
        }
    }
    class InterfaceSegregationPrinciple
    {
        /// <summary>
        /// Es mejor tener interfaces pequeñas y especificas que clases complejas.
        /// </summary>
        /// 
        public interface IFlyable
        {
            void Fly();
        }

        public interface IRangedAttacker
        {
            void Shoot();
        }

        // Enemigos concretos implementan solo lo que necesitan
        public class Dragon : IFlyable, IRangedAttacker
        {
            public void Fly()
            {
                Debug.Log("El dragon vuela");
            }

            public void Shoot()
            {
                Debug.Log("El dragon ejecuta un ataque de rango");
            }
        }

        public class Archer : IRangedAttacker
        {
            public void Shoot()
            {
                Debug.Log("El arquero dispara una flecha");
            }
        }

    }
    class DependencyInversionPrinciple
    {
        /// <summary>
        /// Depender de abstracciones, no de implementaciones concretas.
        /// </summary>
        /// 
        public interface IWeapon
        {
            void Attack();
        }

        public class Sword : IWeapon
        {
            public void Attack()
            {
                Debug.Log("Ataque con espada");
            }
        }

        public class Gun : IWeapon
        {
            public void Attack()
            {
                Debug.Log("Ataque de disparo");
            }
        }

        public class Player
        {
            private IWeapon weapon;

            // Inyección de dependencia
            public Player(IWeapon weapon)
            {
                this.weapon = weapon;
            }

            public void Attack()
            {
                weapon.Attack();
            }
        }

        //Player no depende de una clase especficica o concreta, sino de la abstracción IWeapon.
    }
}
