# Flat list search

Given a flat ordered list, these extensions can get all the children, first level children, root and parent.

## Usage

For these examples, we're using a ordered list with this structure:

```text
.
├── 1
│   └── 2
│       └── 3
│           └── 4
├── 5
│   ├── 6
│   └── 7
│       ├── 8
│       └── 9
│           └── 10
└── 11
    ├── 12
    ├── 13
    │   ├── 14
    └── 15

```

Where the numbers are the Ids.

### Getting all children

In this scenario, the method will return ALL children from id 1.

```csharp
var children = myList.FindChildren(x => x.Id == 1);
```

```text
children:
.
├── 2
│   └── 3
│       └── 4
```

### Getting first level children

In this scenario, the method will return ONLY children where the level is equal to level + 1.

```csharp
var children = myList.FindFirstLevelChildren(x => x.Id == 11);
```

```text
children:
.
├── 11
    ├── 12
    ├── 13
    └── 15
```

### Finding a parent

Returns the parent of the desired item.
If the item DOES NOT contains a parent, it returns null.

```csharp
var parent1 = myList.FindParent(x => x.Id == 4);
var parent2 = myList.FindParent(x => x.Id == 1);
```

```text
parent1:
.
├── 3

parent2:
.null
```

### Finding a root

Returns the root item of the desired item.
If the item IS THE ROOT, it will return itself.

```csharp
var root1 = myList.FindRoot(x => x.Id == 4);
var root2 = myList.FindRoot(x => x.Id == 1);
```

```text
root1:
.
├── 1

root2:
.
├── 1
```
