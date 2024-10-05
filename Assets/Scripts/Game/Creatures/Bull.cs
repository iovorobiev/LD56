
using Game;
using Game.Creatures;
using UnityEngine;
using Utils;

public class Bull : Draggable, Awakable
{

    public float speed = 0f;
    
    
    public Mode currentMode = Mode.SLEEP;
    private Vector3 direction = Vector3.zero;

    private Carriable carryingCreature;
    private Vector3 originalPosition;
    private Transform originalParent;

    private void Start()
    {
        originalParent = transform.parent;
        originalPosition = transform.position;
        if (currentMode == Mode.AWAITING)
        {
            setDraggableActive(true);
        }
    }

    public override void PausableUpdate()
    {
        if (currentMode != Mode.CHARGE)
        {
            return;
        }
        transform.position += direction * (speed * Time.deltaTime);
    }

    public override void PausableFixedUpdate()
    {
        base.PausableFixedUpdate();
        if (currentMode != Mode.IDLE)
        {
            return;
        }
        if (FindTrigger(Vector3.up))
        {
            return;
        }
        if (FindTrigger(Vector3.right))
        {
            return;
        }
        if (FindTrigger(Vector3.down))
        {
            return;
        }
        FindTrigger(Vector3.left);
    }

    private bool FindTrigger(Vector3 searchDirection)
    {
        // Everything except bull itself
        int layermask = LayersHelper.GetAllLayersExcept(Layers.BULL);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, searchDirection,  Mathf.Infinity, layermask);
        if (hit.collider != null && checkHit(hit.collider))
        {
            direction = searchDirection;
            currentMode = Mode.CHARGE;
            return true;
        }

        return false;
    }

    private bool checkHit(Collider2D collider2D)
    {
        var trigger = collider2D.GetComponent<BullTrigger>();
        if (trigger == null)
        {
            return false;
        }
        return trigger.doesTriggerBull();
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == Layers.WALL || other.gameObject.layer == Layers.BULL)
        {
            Debug.Log("Switching mode to idle");
            currentMode = Mode.IDLE;
        }

        if (other.gameObject.layer == Layers.CREATURES)
        {
            if (currentMode != Mode.CHARGE)
            {
                return;
            }
            onCreatureTaken(other.gameObject.GetComponent<Carriable>());
        }
    }

    private void onCreatureTaken(Carriable creature)
    {
        carryingCreature = creature;

        if (direction == Vector3.down || direction == Vector3.up)
        {
            carryingCreature.transform.position =
                new Vector2(transform.position.x, carryingCreature.transform.position.y);
        } else if (direction == Vector3.left || direction == Vector3.right)
        {
            carryingCreature.transform.position =
                new Vector2(carryingCreature.transform.position.x, transform.position.y);
        }
        creature.takenByBull(this);
    }

    public void onCarriedMetCollision()
    {
        currentMode = Mode.FINISHED;
        gameObject.layer = Layers.CREATURES;
        carryingCreature.releasedByBull();
        var closestPos = grid.WorldToCell(transform.position);
        transform.position =  grid.GetCellCenterWorld(closestPos);
    }

    public void AwakeAndWork()
    {
        currentMode = Mode.IDLE;
    }

    public void ResetPositionAndState()
    {
        carryingCreature = null;
        currentMode = Mode.SLEEP;
        transform.position = originalPosition;
        transform.parent = originalParent;
    }

    public override void SettleAtTile(Vector3 getCellCenterWorld)
    {
        base.SettleAtTile(getCellCenterWorld);
        currentMode = Mode.SLEEP;
    }

    public enum Mode
    {
        IDLE,
        CHARGE,
        FINISHED,
        SLEEP,
        AWAITING
    }
}
