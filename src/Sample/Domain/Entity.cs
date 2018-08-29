using System;
using System.Collections.Generic;
using System.Text;
using Admonish;

namespace Domain
{
    public class Entity
    {
        public Entity(int age, string name)
        {
            Validator
                .Create()
                .Min(nameof(age), age, 0)
                .NonNullOrWhiteSpace(nameof(name), name)
                .Check(!(name == "Dracula" && age < 100), "Cannot create such a young vampire.")
                .ThrowIfInvalid();

            Age = age;
            Name = name;
        }

        public string Name { get; }
        public int Age { get; }
    }
}
