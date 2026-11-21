# Coding Rules

## Naming

- Follow Microsoft C# Coding Convention
- Private fields: _camelCase
- Protected/Internal fields: camelCase
- Public fields/properties: PascalCase
- Methods: PascalCase
- Interfaces: Prefix with I

## Architecture

- Namespace required
- No Singleton
- No FindObjectOfType
- No GameObject.Find
- No Resources.Load

- Dependencies must be injected externally
- Do not create service dependencies with new

- Follow single responsibility principle
- Prefer composition over inheritance

## Performance

Inside Update/FixedUpdate/LateUpdate:

- No LINQ
- No string allocations
- No temporary collections
- No unnecessary allocations

Prefer event-driven architecture over polling.

## Async

- Prefer UniTask over Coroutine
- Use Awaitable if not have UniTask library

## UNITY SPECIAL RULES

- Use Unity new input system input legacy input system