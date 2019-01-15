package chuhan.gsp.main;

/**
 * 有向无圈图的节点
 *
 * @param <T>
 * 
 * 其实T未必要实现Comparable接口。暂时先这样吧
 */
public class DAGNode<T extends Comparable<T>> implements Comparable<DAGNode<T> > {
	private final T name;
	private final java.util.ArrayList<DAGNode<T> > prevs;
	private final java.util.ArrayList<DAGNode<T> > nexts;
	private final DAG<T> dag;
	
	
	
	public java.util.ArrayList<DAGNode<T> > getPrevs() {
		return prevs;
	}

	public java.util.ArrayList<DAGNode<T> > getNexts() {
		return nexts;
	}

	public T getName() {
		return name;
	}

	DAGNode(T name,DAG<T> dag) {
		super();
		this.name = name;
		prevs=new java.util.ArrayList<DAGNode<T> >();
		nexts=new java.util.ArrayList<DAGNode<T> >();
		this.dag=dag;
	}
	
	public DAGNode<T> addPrev(T name){		
		final DAGNode<T> p=dag.createNodeIfNotExist(name);
		prevs.add(p);
		p.nexts.add(this);
		return this;
	}
	

	@Override
	public int compareTo(DAGNode<T> o) {		
		return this.name.compareTo(o.name);
	}
	
	@Override
	public int hashCode() {
		return this.name.hashCode();
	}
}
